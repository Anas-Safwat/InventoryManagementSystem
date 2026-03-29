using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.DTOs.ProductDTOs
{
    public class ProductFilterDto : Pager
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? InStock { get; set; }

        public string? SortBy { get; set; }
        //public bool IsAscending { get; set; } = true;
    }
}
