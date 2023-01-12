using System.Linq.Expressions;
using NomadEvents.MinimalAPI.Domain.Entities;

namespace NomadEvents.MinimalAPI.Infrastructure;

public interface IRepository<T> where T : BaseEntity
{
    public Task Add(T entity);
    public Task<T?> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellation);
    public Task<List<T>> GetMany(Expression<Func<T, bool>> predicate, CancellationToken cancellation);
    public Task<int> SaveChangeAsync(CancellationToken cancellationToken);
}