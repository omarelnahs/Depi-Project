using Core.Repositories;
using DataAccessLayer.Data;
using Infrastructure.Repositories;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork : Core.Repositories.IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }

        public UnitOfWork(AppDbContext context,
                          ICategoryRepository categoryRepository,
                          IProductRepository productRepository)
        {
            _context = context;
            Categories = categoryRepository;
            Products = productRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
