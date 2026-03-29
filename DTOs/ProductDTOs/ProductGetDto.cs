using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTOs.ProductDTOs
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public string CategoryName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;

    }
}
