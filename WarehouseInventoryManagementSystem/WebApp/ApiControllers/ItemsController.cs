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
using Newtonsoft.Json;
using Public.DTO.Mappers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IAppBLL _bll;
        private readonly ItemMapper _mapper;

        public ItemsController(ILogger<ItemsController> logger, IMapper autoMapper,
            IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new ItemMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/Items 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.Item>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Item>>> GetItems()
        {
            var res = await _bll.ItemService.AllAsyncWithData();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get item by Id
        /// </summary>
        /// <param name="id">Id of specific item</param>
        /// <returns>Item with specified Id or 404 Not found</returns>
        // GET: api/Items/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Item), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Item>> GetItem(Guid id)
        {
            var item = await _bll.ItemService.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(item));
        }

        /// <summary>
        /// Edit item by id
        /// </summary>
        /// <param name="id">Id of specific item</param>
        /// <param name="item">Id of specific item</param>
        /// <returns></returns>
        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id,
            Public.DTO.v1.Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(item);
            if (mappedEntity == null)
            {
                return BadRequest("No enough data");
            }
            _bll.ItemService.Update(mappedEntity!);

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="item">Item object</param>
        /// <returns>return new Item with id</returns>
        // POST: api/Items
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Item), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Item>> PostItem(
            Public.DTO.v1.Item item)
        {
            var mappedItem = _mapper.Map(item);
            if (mappedItem == null)
            {
                return BadRequest("No enough data");
            }
            var res = _bll.ItemService.Add(mappedItem);
            item.Id = res.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetItem", new { id = res.Id }, item);
        }

        /// <summary>
        /// Delete Item by Id
        /// </summary>
        /// <param name="id">Id of deleted item</param>
        /// <returns></returns>
        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var item = await _bll.ItemService.RemoveAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}