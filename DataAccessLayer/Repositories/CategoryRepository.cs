using Core.Entities;
using Core.Repositories;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Category>();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            await _dbSet.AddAsync(category);
        }

        public void Update(Category category)
        {
            _dbSet.Update(category);
        }

        public void Delete(Category category)
        {
            _dbSet.Remove(category);
        }
    }
}
