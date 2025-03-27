using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
