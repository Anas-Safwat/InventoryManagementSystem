using AutoMapper;
using InventoryManagementSystem.DTOs.CategoryDTOs;
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
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGetDto>>> GetCategories()
        {
            var listOfCategories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CategoryGetDto>>(listOfCategories);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetDto>> GetCategoryById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsyn(id);
            if (category == null)
            {
                return NotFound("Product not found");
            }
            var result = _mapper.Map<CategoryGetDto>(category);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryAddDto newCategoryDto)
        {
            var categoryModel = _mapper.Map<Category>(newCategoryDto);
            await _unitOfWork.CategoryRepository.AddAsync(categoryModel);
            await _unitOfWork.SaveChangesAsync();
            var result = _mapper.Map<CategoryAddDto>(categoryModel);

            return CreatedAtAction(nameof(AddCategory), new { id = categoryModel.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto updateCategoryDto)
        {
            if (id != updateCategoryDto.Id)
            {
                return BadRequest("Category ID in the URL does not match the ID in the request body.");
            }

            var existingCategory = await _unitOfWork.CategoryRepository.GetByIdAsyn(id);

            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            _mapper.Map(updateCategoryDto, existingCategory);

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
