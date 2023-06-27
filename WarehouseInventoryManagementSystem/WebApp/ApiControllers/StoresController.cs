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
    public class StoresController : ControllerBase
    {
        private readonly ILogger<StoresController> _logger;
        private readonly IAppBLL _bll;
        private readonly StoreMapper _mapper;

        public StoresController(ILogger<StoresController> logger, IMapper autoMapper,
            IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new StoreMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/Stores 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.Store>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Store>>> GetStores()
        {
            var res = await _bll.StoreService.AllAsync();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get store by Id
        /// </summary>
        /// <param name="id">Id of specific store</param>
        /// <returns>Store with specified Id or 404 Not found</returns>
        // GET: api/Stores/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Store), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Store>> GetStore(Guid id)
        {
            var store = await _bll.StoreService.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(store));
        }

        /// <summary>
        /// Edit store by id
        /// </summary>
        /// <param name="id">Id of specific store</param>
        /// <param name="store">Id of specific store</param>
        /// <returns></returns>
        // PUT: api/Stores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(Guid id,
            Public.DTO.v1.Store store)
        {
            if (id != store.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(store);
            if (mappedEntity == null)
            {
                return BadRequest("No enough data");
            }
            _bll.StoreService.Update(mappedEntity!);

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new store
        /// </summary>
        /// <param name="store">Store object</param>
        /// <returns>return new Store with id</returns>
        // POST: api/Stores
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Store), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Store>> PostStore(
            Public.DTO.v1.Store store)
        {
            var mappedStore = _mapper.Map(store);
            if (mappedStore == null)
            {
                return BadRequest("No enough data");
            }
            var res = _bll.StoreService.Add(mappedStore);
            store.Id = res.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetStore", new { id = res.Id }, store);
        }

        /// <summary>
        /// Delete Store by Id
        /// </summary>
        /// <param name="id">Id of deleted store</param>
        /// <returns></returns>
        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var store = await _bll.StoreService.RemoveAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}