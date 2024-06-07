using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Primitives;
namespace Infrastructure.Data.Repositories;
public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly LibraryDBContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public Repository(LibraryDBContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<T>();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public Task UpdateAsync(T entity)
    {
        return Task.Run(() => { 
            _dbSet.Update(entity);
        });
    }
}
