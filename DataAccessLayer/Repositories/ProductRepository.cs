using Core.Entities;
using DataAccessLayer.Data;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Set<Product>().Where(p => p.CategoryId == categoryId).ToListAsync();
        }
    }
}
