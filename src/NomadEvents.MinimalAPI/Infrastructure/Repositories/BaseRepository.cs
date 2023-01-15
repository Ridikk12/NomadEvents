using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NomadEvents.MinimalAPI.Domain.Entities;

namespace NomadEvents.MinimalAPI.Infrastructure;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected DbContext _dbContext;

    protected BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public Task<T?> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellation)
    {
        return _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellation);
    }

    public Task<List<T>> GetMany(Expression<Func<T, bool>> predicate, CancellationToken cancellation)
    {
        return _dbContext.Set<T>().Where(predicate).AsNoTracking().ToListAsync(cancellation);
    }

    public Task<int> SaveChangeAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}