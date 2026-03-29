using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.GenericRepositories;
namespace InventoryManagementSystem.Repositories.SupplierRepositories
{
    public class SupplierRepository : GenericRepository<Supplier> , ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context) { }
    }
}
