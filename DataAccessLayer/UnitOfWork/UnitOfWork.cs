using DataAccessLayer.Data;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }

        public UnitOfWork(AppDbContext context, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            Products = productRepository;
            Categories = categoryRepository;
        }

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public void Dispose() => _context.Dispose();
    }
}
