using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.CategoryRepositories;
using InventoryManagementSystem.Repositories.GenericRepositories;
using InventoryManagementSystem.Repositories.ProductRepositories;

namespace InventoryManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IProductRepository _productRepository { get; }
        private ICategoryRepository _categoryRepository { get; }
        private IGenericRepository<Supplier> _supplierRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IProductRepository ProductRepository
            => _productRepository ?? new ProductRepository(_context);

        public ICategoryRepository CategoryRepository
            => _categoryRepository ?? new CategoryRepository(_context);

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
