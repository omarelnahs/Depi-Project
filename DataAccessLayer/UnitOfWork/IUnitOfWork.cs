using Core.Repositories;

namespace Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        Task<bool> SaveChangesAsync();
    }
}
