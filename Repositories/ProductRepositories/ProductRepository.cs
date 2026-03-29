using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTOs.ProductDTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagementSystem.Repositories.ProductRepositories
{
    public class ProductRepository :GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }


        public async Task<IEnumerable<Product>> GetFilteredProducts(ProductFilterDto filter)
        {
            IQueryable<Product> query = _dbSet
                .Include(p=>p.Category)
                .Include(p=>p.Supplier)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(term =>
                term.Name.ToLower().Contains(filter.SearchTerm.ToLower()) ||
                term.Description.ToLower().Contains(filter.SearchTerm.ToLower())
                );
            }

            if (filter.SupplierId.HasValue)
            {
                query = query.Where(term => term.SupplierId == filter.SupplierId);
            }

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(term => term.CategoryId == filter.CategoryId);
            }
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(term => term.Price >= filter.MinPrice);

            }
            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(term => term.Price <= filter.MaxPrice);

            }
            if (filter.InStock.HasValue)
            {
                query = filter.InStock.Value
                    ? query.Where(term => term.StockQuantity > 0)
                    : query.Where(term => term.StockQuantity == 0);
            }


            if (!string.IsNullOrWhiteSpace(filter.SortBy))
            {
                var sortParams = filter.SortBy.Split(',').Select(p => p.Trim().ToLower()).ToList();

                IOrderedQueryable<Product>? orderedQuery = null;

                for (int i = 0; i < sortParams.Count; i++)
                {
                    var currentSort = sortParams[i];

                    bool isDescending = currentSort.EndsWith(" desc");

                    var fieldName = currentSort.Replace(" desc", "").Replace(" asc", "").Trim();

                    if (i == 0)
                    {
                        switch (fieldName)
                        {
                            case "name": orderedQuery = isDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name); break;
                            case "price": orderedQuery = isDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price); break;
                            case "stock": orderedQuery = isDescending ? query.OrderByDescending(p => p.StockQuantity) : query.OrderBy(p => p.StockQuantity); break;
                            default: orderedQuery = query.OrderBy(p => p.Id); break;
                        }
                    }
                    else if (orderedQuery != null)
                    {
                        switch (fieldName)
                        {
                            case "name": orderedQuery = isDescending ? orderedQuery.ThenByDescending(p => p.Name) : orderedQuery.ThenBy(p => p.Name); break;
                            case "price": orderedQuery = isDescending ? orderedQuery.ThenByDescending(p => p.Price) : orderedQuery.ThenBy(p => p.Price); break;
                            case "stock": orderedQuery = isDescending ? orderedQuery.ThenByDescending(p => p.StockQuantity) : orderedQuery.ThenBy(p => p.StockQuantity); break;
                        }
                    }
                }

                query = orderedQuery ?? query.OrderBy(p => p.Id);
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }

            if (filter.isValid)
            {
                query = query.Skip(filter.Skip).Take(filter.PageSize);
            }

            return await query.ToListAsync();


        }
    }
}
