using InventoryManagementSystem.Repositories.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Data;
namespace InventoryManagementSystem.Repositories.ProductRepositories
{
    public class ProductRepository :GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

    }
}
