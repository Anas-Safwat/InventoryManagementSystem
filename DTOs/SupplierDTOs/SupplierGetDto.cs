using InventoryManagementSystem.Models;
using InventoryManagementSystem.DTOs.ProductDTOs;
namespace InventoryManagementSystem.DTOs.SupplierDTOs
{
    public class SupplierGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

    }
}
