using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagementSystem.Repositories.CategoryRepositories
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<bool> IsNameUniqueAsync(string name, int? excludeId = null)
        {
            var query = _dbSet.Where(c => c.Name.ToLower() == name.ToLower());

            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }

            return !await query.AnyAsync();
        }
    }

}
