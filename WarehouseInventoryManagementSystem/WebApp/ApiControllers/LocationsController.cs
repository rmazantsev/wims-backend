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
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly IAppBLL _bll;
        private readonly LocationMapper _mapper;

        public LocationsController(ILogger<LocationsController> logger, IMapper autoMapper,
            IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new LocationMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/Locations 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.Location>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Location>>> GetLocations()
        {
            var res = await _bll.LocationService.AllAsync();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get location by Id
        /// </summary>
        /// <param name="id">Id of specific location</param>
        /// <returns>Location with specified Id or 404 Not found</returns>
        // GET: api/Locations/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Location), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Location>> GetLocation(Guid id)
        {
            var location = await _bll.LocationService.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(location));
        }

        /// <summary>
        /// Edit location by id
        /// </summary>
        /// <param name="id">Id of specific location</param>
        /// <param name="location">Id of specific location</param>
        /// <returns></returns>
        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(Guid id,
            Public.DTO.v1.Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(location);
            if (mappedEntity == null)
            {
                return BadRequest("No enough data");
            }
            _bll.LocationService.Update(mappedEntity!);

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new location
        /// </summary>
        /// <param name="location">Location object</param>
        /// <returns>return new Location with id</returns>
        // POST: api/Locations
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Location), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Location>> PostLocation(
            Public.DTO.v1.Location location)
        {
            var mappedLocation = _mapper.Map(location);
            if (mappedLocation == null)
            {
                return BadRequest("No enough data");
            }
            var res = _bll.LocationService.Add(mappedLocation);
            location.Id = res.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetLocation", new { id = res.Id }, location);
        }

        /// <summary>
        /// Delete Location by Id
        /// </summary>
        /// <param name="id">Id of deleted location</param>
        /// <returns></returns>
        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var location = await _bll.LocationService.RemoveAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}