using InventoryManagementSystem.Models;
using InventoryManagementSystem.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsyn(id);
            if (category == null)
            {
                return NotFound("Product not found");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category newCategory)
        {
            await _unitOfWork.CategoryRepository.AddAsync(newCategory);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(AddCategory), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category newCategory)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetByIdAsyn(id);
            if (existingCategory == null)
            {
                return NotFound("Product not found");
            }
            existingCategory.Id = newCategory.Id;
            existingCategory.Name = newCategory.Name;
            existingCategory.Description = newCategory.Description;
            existingCategory.Products = newCategory.Products;

            _unitOfWork.CategoryRepository.Update(existingCategory);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsyn(id);
            if (category == null)
            {
                return NotFound("Product not found");
            }
            _unitOfWork.CategoryRepository.Remove(category);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
