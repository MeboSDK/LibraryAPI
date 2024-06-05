using System.Linq.Expressions;

namespace Domain.Abstractions;
public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
}
