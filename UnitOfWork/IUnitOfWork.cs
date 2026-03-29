using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.CategoryRepositories;
using InventoryManagementSystem.Repositories.GenericRepositories;
using InventoryManagementSystem.Repositories.ProductRepositories;

namespace InventoryManagementSystem.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IGenericRepository<Supplier> SupplierRepository { get; }

        Task<int> SaveChangesAsync();

        bool HasChanges();
    }
}
