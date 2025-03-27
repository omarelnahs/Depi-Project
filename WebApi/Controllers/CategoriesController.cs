using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (category == null) return BadRequest();
            await _categoryRepository.AddAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (id != category.Id) return BadRequest();
            _categoryRepository.Update(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return NotFound();
            _categoryRepository.Delete(category);
            return NoContent();
        }
    }
}
