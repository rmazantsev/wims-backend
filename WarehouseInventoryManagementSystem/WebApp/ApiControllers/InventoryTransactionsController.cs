#pragma warning disable 1591
using System.Net.Mime;
using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InventoryTransactionsController : ControllerBase
    {
        private readonly ILogger<InventoryTransactionsController> _logger;
        private readonly IAppBLL _bll;
        private readonly InventoryTransactionMapper _mapper;

        public InventoryTransactionsController(ILogger<InventoryTransactionsController> logger, IMapper autoMapper,
            IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new InventoryTransactionMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/InventoryTransactions 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.InventoryTransaction>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.InventoryTransaction>>> GetInventoryTransactions()
        {
            var res = await _bll.InventoryTransactionService.AllAsync();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get inventoryTransaction by Id
        /// </summary>
        /// <param name="id">Id of specific inventoryTransaction</param>
        /// <returns>InventoryTransaction with specified Id or 404 Not found</returns>
        // GET: api/InventoryTransactions/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.InventoryTransaction), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.InventoryTransaction>> GetInventoryTransaction(Guid id)
        {
            var inventoryTransaction = await _bll.InventoryTransactionService.FindAsync(id);

            if (inventoryTransaction == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(inventoryTransaction));
        }

        /// <summary>
        /// Edit inventoryTransaction by id
        /// </summary>
        /// <param name="id">Id of specific inventoryTransaction</param>
        /// <param name="inventoryTransaction">Id of specific inventoryTransaction</param>
        /// <returns></returns>
        // PUT: api/InventoryTransactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryTransaction(Guid id,
            Public.DTO.v1.InventoryTransaction inventoryTransaction)
        {
            if (id != inventoryTransaction.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(inventoryTransaction);
            if (mappedEntity == null)
            {
                return BadRequest("No enough data");
            }
            _bll.InventoryTransactionService.Update(mappedEntity!);

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new inventoryTransaction
        /// </summary>
        /// <param name="inventoryTransaction">InventoryTransaction object</param>
        /// <returns>return new InventoryTransaction with id</returns>
        // POST: api/InventoryTransactions
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.InventoryTransaction), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.InventoryTransaction>> PostInventoryTransaction(
            Public.DTO.v1.InventoryTransaction inventoryTransaction)
        {
            var mappedInventoryTransaction = _mapper.Map(inventoryTransaction);
            if (mappedInventoryTransaction == null)
            {
                return BadRequest("No enough data");
            }
            var res = _bll.InventoryTransactionService.Add(mappedInventoryTransaction);
            inventoryTransaction.Id = res.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetInventoryTransaction", new { id = res.Id }, inventoryTransaction);
        }

        /// <summary>
        /// Delete InventoryTransaction by Id
        /// </summary>
        /// <param name="id">Id of deleted inventoryTransaction</param>
        /// <returns></returns>
        // DELETE: api/InventoryTransactions/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteInventoryTransaction(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var inventoryTransaction = await _bll.InventoryTransactionService.RemoveAsync(id);
            if (inventoryTransaction == null)
            {
                return NotFound();
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}