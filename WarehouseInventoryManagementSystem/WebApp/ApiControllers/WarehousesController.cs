#pragma warning disable 1591
using System.Net.Mime;
using System.Text.Json;
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
    public class WarehousesController : ControllerBase
    {
        private readonly ILogger<WarehousesController> _logger;
        private readonly IAppBLL _bll;
        private readonly WarehouseMapper _mapper;

        public WarehousesController(ILogger<WarehousesController> logger, IMapper autoMapper,
            IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new WarehouseMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/Warehouses 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.Warehouse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Warehouse>>> GetWarehouses()
        {
            var res = await _bll.WarehouseService.AllAsync();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get warehouse by Id
        /// </summary>
        /// <param name="id">Id of specific warehouse</param>
        /// <returns>Warehouse with specified Id or 404 Not found</returns>
        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Warehouse), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Warehouse>> GetWarehouse(Guid id)
        {
            var warehouse = await _bll.WarehouseService.FindAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(warehouse));
        }

        /// <summary>
        /// Edit warehouse by id
        /// </summary>
        /// <param name="id">Id of specific warehouse</param>
        /// <param name="warehouse">Id of specific warehouse</param>
        /// <returns></returns>
        // PUT: api/Warehouses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(Guid id,
            Public.DTO.v1.Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(warehouse);
            if (mappedEntity == null)
            {
                return BadRequest("No enough data");
            }

            _bll.WarehouseService.Update(mappedEntity!);

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new warehouse
        /// </summary>
        /// <param name="warehouse">Warehouse object</param>
        /// <returns>return new Warehouse with id</returns>
        // POST: api/Warehouses
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Warehouse), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Warehouse>> PostWarehouse(
            Public.DTO.v1.Warehouse warehouse)
        {
            var mappedWarehouse = _mapper.Map(warehouse);
            if (mappedWarehouse == null)
            {
                return BadRequest("No enough data");
            }

            var res = _bll.WarehouseService.Add(mappedWarehouse);
            warehouse.Id = res.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetWarehouse", new { id = res.Id }, warehouse);
        }

        /// <summary>
        /// Delete Warehouse by Id
        /// </summary>
        /// <param name="id">Id of deleted warehouse</param>
        /// <returns></returns>
        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var warehouse = await _bll.WarehouseService.CheckAndRemoveAsync(id);
            if (warehouse == null)
            {
                return BadRequest("Cannot delete warehouse with inventory");
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}