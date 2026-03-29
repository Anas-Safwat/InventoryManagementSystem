using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.GenericRepositories;

namespace InventoryManagementSystem.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
         IGenericRepository<Product> ProductRepository { get; }
         IGenericRepository<Category> CategoryRepository { get; }
         IGenericRepository<Supplier> SupplierRepository { get; }

        Task<int> SaveChangesAsync();

        bool HasChanges();
    }
}
