using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTOs.SupplierDTOs
{
    public class SupplierAddDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string ContactEmail { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;
    }
}
