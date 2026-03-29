using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTOs.CategoryDTOs
{
    public class CategoryAddDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
