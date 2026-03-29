using InventoryManagementSystem.Models;
using InventoryManagementSystem.UnitOfWork;
using InventoryManagementSystem.DTOs.SupplierDTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SuppliersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierGetDto>>> GetSuppliers()
        {
            var suppliers = await _unitOfWork.SupplierRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<SupplierGetDto>>(suppliers);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierGetDto>> GetSupplierById(int id)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsyn(id);
            if (supplier == null)
            {
                return NotFound("Product not found");
            }
            var result = _mapper.Map<SupplierGetDto>(supplier);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] SupplierAddDto newSupplierDto)
        {
            var supplierModel = _mapper.Map<Supplier>(newSupplierDto);
            await _unitOfWork.SupplierRepository.AddAsync(supplierModel);
            await _unitOfWork.SaveChangesAsync();
            var result = _mapper.Map<SupplierAddDto>(supplierModel);
            return CreatedAtAction(nameof(AddSupplier), new { id = supplierModel.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] SupplierAddDto updateSupplierDto)
        {
            var existingSupplier = await _unitOfWork.SupplierRepository.GetByIdAsyn(id);
            if (existingSupplier == null)
            {
                return NotFound("Product not found");
            }
            _mapper.Map(updateSupplierDto, existingSupplier);
            
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
