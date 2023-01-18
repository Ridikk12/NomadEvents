using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NomadEvents.MinimalAPI.Domain.Entities;

namespace NomadEvents.MinimalAPI.Infrastructure;

public class NomadEventDbContext : DbContext
{
    public NomadEventDbContext(DbContextOptions<NomadEventDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<NomadEvent> Events { get; set; }
}

public class NomadEventEntityConfiguration : IEntityTypeConfiguration<NomadEvent>
{
    public void Configure(EntityTypeBuilder<NomadEvent> builder)
    {
    
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Users);
        builder.Property(x => x.Price).HasPrecision(19,4);
    }
}