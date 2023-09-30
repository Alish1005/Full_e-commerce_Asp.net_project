using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Project.Models;

namespace Project.Data.Repository
{
    public interface IEntityRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        
        Task AddAsync(T obj);
        Task<T> UpdateAsync(object id, T obj);
        Task DeleteAsync(int id);

    }
}
