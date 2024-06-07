using System.Linq.Expressions;

namespace Domain.Abstractions;
public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
}
