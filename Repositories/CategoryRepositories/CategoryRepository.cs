using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.GenericRepositories;
namespace InventoryManagementSystem.Repositories.CategoryRepositories
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    }

}
