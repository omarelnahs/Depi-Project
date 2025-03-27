using Core.Repositories;

namespace Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        Task<int> CompleteAsync();
    }
}
