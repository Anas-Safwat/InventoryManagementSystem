using InventoryManagementSystem.Models;
using InventoryManagementSystem.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SuppliersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            var suppliers = await _unitOfWork.SupplierRepository.GetAllAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplierById(int id)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsyn(id);
            if (supplier == null)
            {
                return NotFound("Product not found");
            }
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] Supplier newSupplier)
        {
            await _unitOfWork.SupplierRepository.AddAsync(newSupplier);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(AddSupplier), new { id = newSupplier.Id }, newSupplier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Supplier newSupplier)
        {
            var existingSupplier = await _unitOfWork.SupplierRepository.GetByIdAsyn(id);
            if (existingSupplier == null)
            {
                return NotFound("Product not found");
            }
            existingSupplier.Id = newSupplier.Id;
            existingSupplier.Name = newSupplier.Name;
            existingSupplier.ContactEmail = newSupplier.ContactEmail;
            existingSupplier.Phone = newSupplier.Phone;
            existingSupplier.Products = newSupplier.Products;

            _unitOfWork.SupplierRepository.Update(existingSupplier);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSupplier(int id)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsyn(id);
            if (supplier == null)
            {
                return NotFound("Product not found");
            }
            _unitOfWork.SupplierRepository.Remove(supplier);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
