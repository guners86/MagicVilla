using System.Linq.Expressions;

namespace MagicVilla.API.Repository.Services
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? predicate = null, bool tracked = true);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
