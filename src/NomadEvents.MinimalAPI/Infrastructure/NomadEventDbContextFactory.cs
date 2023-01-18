using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NomadEvents.MinimalAPI.Infrastructure;

//Needed for migration
public class NomadEventDbContextFactory : IDesignTimeDbContextFactory<NomadEventDbContext>
{
    public NomadEventDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NomadEventDbContext>();
        optionsBuilder.UseSqlServer(@"Server=.\;Database=NomadEvents;TrustServerCertificate=True;Integrated Security=true;");

        return new NomadEventDbContext(optionsBuilder.Options);
    }
}