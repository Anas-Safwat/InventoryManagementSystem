using InventoryManagementSystem.DTOs.ProductDTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.GenericRepositories;

namespace InventoryManagementSystem.Repositories.ProductRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
         Task<IEnumerable<Product>> GetFilteredProducts(ProductFilterDto filter);
    }
}
