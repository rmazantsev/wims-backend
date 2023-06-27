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
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IAppBLL _bll;
        private readonly CategoryMapper _mapper;

        public CategoriesController(ILogger<CategoriesController> logger, IMapper autoMapper, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
            _mapper = new CategoryMapper(autoMapper);
        }

        /// <summary>
        /// Get all categories 
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/Categories 
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Public.DTO.v1.Category>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Category>>> GetCategories()
        {
            var res = await _bll.CategoryService.AllAsync();
            var data = res
                .Select(c => _mapper.Map(c))
                .ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get category by Id
        /// </summary>
        /// <param name="id">Id of specific category</param>
        /// <returns>Category with specified Id or 404 Not found</returns>
        // GET: api/Categories/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Category), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Category>> GetCategory(Guid id)
        {
            var category = await _bll.CategoryService.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(category));
        }

        /// <summary>
        /// Edit category by id
        /// </summary>
        /// <param name="id">Id of specific category</param>
        /// <param name="category">Id of specific category</param>
        /// <returns></returns>
        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Public.DTO.v1.Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var mappedEntity = _mapper.Map(category);
            _bll.CategoryService.Update(mappedEntity!);

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add new category
        /// </summary>
        /// <param name="category">Category object</param>
        /// <returns>return new Category with id</returns>
        // POST: api/Categories
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Public.DTO.v1.Category), StatusCodes.Status200OK)]
        public async Task<ActionResult<Public.DTO.v1.Category>> PostCategory(Public.DTO.v1.Category category)
        {
            var mappedCategory = _mapper.Map(category);

            var res = _bll.CategoryService.Add(mappedCategory!);
            category.Id = res.Id;

            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetCategory", new { id = res.Id }, category);
        }

        /// <summary>
        /// Delete Category by Id
        /// </summary>
        /// <param name="id">Id of deleted category</param>
        /// <returns></returns>
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (!User.IsInAdminRole())
            {
                return BadRequest("Access denied");
            }
            var category = await _bll.CategoryService.RemoveAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}