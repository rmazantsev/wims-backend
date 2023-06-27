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
    public class ItemComponentsController : ControllerBase
    {
        private readonly ILogger<ItemComponentsController> _logger;
        private readonly IAppBLL _bll;
        private readonly ItemComponentMapper _mapper;

        public ItemComponentsController(ILogger<ItemComponentsController> logger, IMapper autoMapper,
            IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new ItemComponentMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/ItemComponents 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.ItemComponent>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.ItemComponent>>> GetItemComponents()
        {
            var res = await _bll.ItemComponentService.AllAsync();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get itemComponent by Id
        /// </summary>
        /// <param name="id">Id of specific itemComponent</param>
        /// <returns>ItemComponent with specified Id or 404 Not found</returns>
        // GET: api/ItemComponents/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.ItemComponent), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.ItemComponent>> GetItemComponent(Guid id)
        {
            var itemComponent = await _bll.ItemComponentService.FindAsync(id);

            if (itemComponent == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(itemComponent));
        }

        /// <summary>
        /// Edit itemComponent by id
        /// </summary>
        /// <param name="id">Id of specific itemComponent</param>
        /// <param name="itemComponent">Id of specific itemComponent</param>
        /// <returns></returns>
        // PUT: api/ItemComponents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemComponent(Guid id,
            Public.DTO.v1.ItemComponent itemComponent)
        {
            if (id != itemComponent.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(itemComponent);
            if (mappedEntity == null)
            {
                return BadRequest("No enough data");
            }
            _bll.ItemComponentService.Update(mappedEntity);

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new itemComponent
        /// </summary>
        /// <param name="itemComponent">ItemComponent object</param>
        /// <returns>return new ItemComponent with id</returns>
        // POST: api/ItemComponents
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.ItemComponent), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.ItemComponent>> PostItemComponent(
            Public.DTO.v1.ItemComponent itemComponent)
        {
            var mappedItemComponent = _mapper.Map(itemComponent);
            if (mappedItemComponent == null)
            {
                return BadRequest("No enough data");
            }
            var res = _bll.ItemComponentService.AddOrUpdate(mappedItemComponent);
            itemComponent.Id = res!.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetItemComponent", new { id = res.Id }, itemComponent);
        }

        /// <summary>
        /// Delete ItemComponent by Id
        /// </summary>
        /// <param name="id">Id of deleted itemComponent</param>
        /// <returns></returns>
        // DELETE: api/ItemComponents/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteItemComponent(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var itemComponent = await _bll.ItemComponentService.RemoveAsync(id);
            if (itemComponent == null)
            {
                return NotFound();
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}