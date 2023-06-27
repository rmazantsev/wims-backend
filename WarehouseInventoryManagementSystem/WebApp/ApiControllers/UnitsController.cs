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
    public class UnitsController : ControllerBase
    {
        private readonly ILogger<UnitsController> _logger;
        private readonly IAppBLL _bll;
        private readonly UnitMapper _mapper;

        public UnitsController(ILogger<UnitsController> logger, IMapper autoMapper,
            IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new UnitMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/Units 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.Unit>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Unit>>> GetUnits()
        {
            var res = await _bll.UnitService.AllAsync();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get unit by Id
        /// </summary>
        /// <param name="id">Id of specific unit</param>
        /// <returns>Unit with specified Id or 404 Not found</returns>
        // GET: api/Units/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Unit), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Unit>> GetUnit(Guid id)
        {
            var unit = await _bll.UnitService.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(unit));
        }

        /// <summary>
        /// Edit unit by id
        /// </summary>
        /// <param name="id">Id of specific unit</param>
        /// <param name="unit">Id of specific unit</param>
        /// <returns></returns>
        // PUT: api/Units/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(Guid id,
            Public.DTO.v1.Unit unit)
        {
            if (id != unit.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(unit);
            if (mappedEntity == null)
            {
                return BadRequest("No enough data");
            }
            _bll.UnitService.Update(mappedEntity!);

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new unit
        /// </summary>
        /// <param name="unit">Unit object</param>
        /// <returns>return new Unit with id</returns>
        // POST: api/Units
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Unit), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Unit>> PostUnit(
            Public.DTO.v1.Unit unit)
        {
            var mappedUnit = _mapper.Map(unit);
            if (mappedUnit == null)
            {
                return BadRequest("No enough data");
            }
            var res = _bll.UnitService.Add(mappedUnit);
            unit.Id = res.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetUnit", new { id = res.Id }, unit);
        }

        /// <summary>
        /// Delete Unit by Id
        /// </summary>
        /// <param name="id">Id of deleted unit</param>
        /// <returns></returns>
        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var unit = await _bll.UnitService.RemoveAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}