using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.GenericRepositories;

namespace InventoryManagementSystem.Repositories.CategoryRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> IsNameUniqueAsync(string name, int? excludeId = null);
    }
}
