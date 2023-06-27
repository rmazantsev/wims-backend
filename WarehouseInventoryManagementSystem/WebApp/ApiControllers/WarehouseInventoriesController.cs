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
    public class WarehouseInventoriesController : ControllerBase
    {
        private readonly ILogger<WarehouseInventoriesController> _logger;
        private readonly IAppBLL _bll;
        private readonly WarehouseInventoryMapper _mapper;

        public WarehouseInventoriesController(ILogger<WarehouseInventoriesController> logger, IMapper autoMapper,
            IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new WarehouseInventoryMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/WarehouseInventories 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.WarehouseInventory>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.WarehouseInventory>>> GetWarehouseInventories()
        {
            var res = await _bll.WarehouseInventoryService.AllAsync();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get warehouseInventory by Id
        /// </summary>
        /// <param name="id">Id of specific warehouseInventory</param>
        /// <returns>WarehouseInventory with specified Id or 404 Not found</returns>
        // GET: api/WarehouseInventories/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.WarehouseInventory), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.WarehouseInventory>> GetWarehouseInventory(Guid id)
        {
            var warehouseInventory = await _bll.WarehouseInventoryService.FindAsyncWithData(id);

            if (warehouseInventory == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(warehouseInventory));
        }

        /// <summary>
        /// Edit warehouseInventory by id
        /// </summary>
        /// <param name="id">Id of specific warehouseInventory</param>
        /// <param name="warehouseInventory">Id of specific warehouseInventory</param>
        /// <returns></returns>
        // PUT: api/WarehouseInventories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouseInventory(Guid id,
            Public.DTO.v1.WarehouseInventory warehouseInventory)
        {
            if (id != warehouseInventory.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(warehouseInventory);
            if (mappedEntity == null)
            {
                return BadRequest("No enough data");
            }
            await _bll.WarehouseInventoryService.UpdateWithTransaction(mappedEntity, User.GetUserId());

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new warehouseInventory
        /// </summary>
        /// <param name="warehouseInventory">WarehouseInventory object</param>
        /// <returns>return new WarehouseInventory with id</returns>
        // POST: api/WarehouseInventories
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.WarehouseInventory), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.WarehouseInventory>> PostWarehouseInventory(
            Public.DTO.v1.WarehouseInventory warehouseInventory)
        {
            var mappedWarehouseInventory = _mapper.Map(warehouseInventory);
            if (mappedWarehouseInventory == null)
            {
                return BadRequest("Not enough data");
            }
            var res = await _bll.WarehouseInventoryService
                .AddWithTransaction(mappedWarehouseInventory, User.GetUserId());
            if (res == null)
            {
                return BadRequest("Not enough data");
            }
            warehouseInventory.Id = res.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetWarehouseInventory", new { id = res.Id }, warehouseInventory);
        }

        /// <summary>
        /// Delete WarehouseInventory by Id
        /// </summary>
        /// <param name="id">Id of deleted warehouseInventory</param>
        /// <returns></returns>
        // DELETE: api/WarehouseInventories/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteWarehouseInventory(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var warehouseInventory = await _bll.WarehouseInventoryService.RemoveAsync(id, User.GetUserId());
            if (warehouseInventory == null)
            {
                return NotFound();
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}