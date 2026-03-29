using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.GenericRepositories;

namespace InventoryManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IGenericRepository<Product> _productRepository { get; }
        private IGenericRepository<Category> _categoryRepository { get; }
        private IGenericRepository<Supplier> _supplierRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<Product> ProductRepository
            => _productRepository ?? new GenericRepository<Product>(_context);

        public IGenericRepository<Category> CategoryRepository
            => _categoryRepository ?? new GenericRepository<Category>(_context);

        public IGenericRepository<Supplier> SupplierRepository
            => _supplierRepository ?? new GenericRepository<Supplier>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
