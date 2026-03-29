using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.GenericRepositories;

namespace InventoryManagementSystem.Repositories.ProductRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<IEnumerable<Product>> GetFilteredProducts();
    }
}
