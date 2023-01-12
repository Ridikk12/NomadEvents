using NomadEvents.MinimalAPI.Domain.Entities;

namespace NomadEvents.MinimalAPI.Infrastructure;

public class NomadEventRepository : BaseRepository<NomadEvent>, INomadEventRepository
{
    public NomadEventRepository(NomadEventDbContext dbContext) : base(dbContext)
    {
        
    }
}