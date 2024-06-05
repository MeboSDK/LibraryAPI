using Domain.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Servicies.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDBContext _dbContext;

    public UnitOfWork(LibraryDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitAsync()
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                int result = await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
