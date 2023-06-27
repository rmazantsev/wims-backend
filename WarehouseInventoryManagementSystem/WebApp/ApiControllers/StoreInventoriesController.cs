#pragma warning disable 1591
using System.Net.Mime;
using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
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
    public class StoreInventoriesController : ControllerBase
    {
        private readonly ILogger<StoreInventoriesController> _logger;
        private readonly IAppBLL _bll;
        private readonly StoreInventoryMapper _mapper;

        public StoreInventoriesController(ILogger<StoreInventoriesController> logger, IMapper autoMapper,
            IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new StoreInventoryMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/StoreInventories 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.StoreInventory>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.StoreInventory>>> GetStoreInventories()
        {
            var res = await _bll.StoreInventoryService.AllAsync();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get storeInventory by Id
        /// </summary>
        /// <param name="id">Id of specific storeInventory</param>
        /// <returns>StoreInventory with specified Id or 404 Not found</returns>
        // GET: api/StoreInventories/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.StoreInventory), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.StoreInventory>> GetStoreInventory(Guid id)
        {
            var storeInventory = await _bll.StoreInventoryService.FindAsyncWithData(id);

            if (storeInventory == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(storeInventory));
        }

        /// <summary>
        /// Edit storeInventory by id
        /// </summary>
        /// <param name="id">Id of specific storeInventory</param>
        /// <param name="storeInventory">Id of specific storeInventory</param>
        /// <returns></returns>
        // PUT: api/StoreInventories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreInventory(Guid id,
            Public.DTO.v1.StoreInventory storeInventory)
        {
            if (id != storeInventory.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(storeInventory);
            if (mappedEntity == null)
            {
                return BadRequest("No enough data");
            }
            await _bll.StoreInventoryService.UpdateWithTransaction(mappedEntity, User.GetUserId());

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new storeInventory
        /// </summary>
        /// <param name="storeInventory">StoreInventory object</param>
        /// <returns>return new StoreInventory with id</returns>
        // POST: api/StoreInventories
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.StoreInventory), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.StoreInventory>> PostStoreInventory(
            Public.DTO.v1.StoreInventory storeInventory)
        {
            var mappedStoreInventory = _mapper.Map(storeInventory);
            if (mappedStoreInventory == null)
            {
                return BadRequest("No enough data");
            }
            var res = await _bll.StoreInventoryService.AddWithTransaction(mappedStoreInventory, User.GetUserId());
            if (res == null)
            {
                return BadRequest("No enough data");
            }
            storeInventory.Id = res.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetStoreInventory", new { id = res.Id }, storeInventory);
        }

        /// <summary>
        /// Delete StoreInventory by Id
        /// </summary>
        /// <param name="id">Id of deleted storeInventory</param>
        /// <returns></returns>
        // DELETE: api/StoreInventories/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteStoreInventory(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var storeInventory = await _bll.StoreInventoryService.RemoveAsync(id, User.GetUserId());
            if (storeInventory == null)
            {
                return NotFound();
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}