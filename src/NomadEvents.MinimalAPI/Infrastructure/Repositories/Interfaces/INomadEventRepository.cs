using NomadEvents.MinimalAPI.Domain.Entities;

namespace NomadEvents.MinimalAPI.Infrastructure;

public interface INomadEventRepository : IRepository<NomadEvent>
{
}