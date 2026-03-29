using AutoMapper;
using InventoryManagementSystem.DTOs.ProductDTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductGetDto>>> GetProducts([FromQuery] ProductFilterDto filter)
        {

            var listOfProducts = await _unitOfWork.ProductRepository.GetFilteredProducts(filter);
            var cleanResult = _mapper.Map<IEnumerable<ProductGetDto>>(listOfProducts);
            return Ok(cleanResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGetDto>> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsyn(id);
            if(product == null)
            {
                return NotFound("Product not found");
            }
            var result = _mapper.Map<ProductGetDto>(product);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductAddDto newProductDto)
        {
            var productModel = _mapper.Map<Product>(newProductDto);
            await _unitOfWork.ProductRepository.AddAsync(productModel);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<ProductGetDto>(productModel);

            return CreatedAtAction(nameof(AddProduct), new { id = productModel.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductAddDto updateProductDto)
        {

            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsyn(id);
            if(existingProduct == null)
            {
                return NotFound("Product not found");
            }
            _mapper.Map(updateProductDto, existingProduct);    
            
            _unitOfWork.ProductRepository.Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsyn(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            _unitOfWork.ProductRepository.Remove(product);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
