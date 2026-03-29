using InventoryManagementSystem.Models;
using InventoryManagementSystem.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products  = await _unitOfWork.ProductRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsyn(id);
            if(product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product newProduct)
        {
            await _unitOfWork.ProductRepository.AddAsync(newProduct);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(AddProduct), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product newProduct)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsyn(id);
            if(existingProduct == null)
            {
                return NotFound("Product not found");
            }
            existingProduct.Id = newProduct.Id;
            existingProduct.Name = newProduct.Name;
            existingProduct.Description = newProduct.Description;
            existingProduct.Price = newProduct.Price;
            existingProduct.StockQuantity = newProduct.StockQuantity;
            existingProduct.CategoryId = newProduct.CategoryId;
            existingProduct.Category = newProduct.Category;
            existingProduct.SupplierId = newProduct.SupplierId;
            existingProduct.Supplier = newProduct.Supplier;

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
