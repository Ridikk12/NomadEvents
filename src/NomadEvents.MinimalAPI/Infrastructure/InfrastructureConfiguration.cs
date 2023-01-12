using Microsoft.EntityFrameworkCore;

namespace NomadEvents.MinimalAPI.Infrastructure;

public static class StartupConfiguration
{
    public static void AddDatabase(this IServiceCollection services, WebApplicationBuilder builder)
    {
        if (bool.Parse(builder.Configuration["UseInMemoryDb"]))
        {
            services.AddEntityFrameworkInMemoryDatabase();
        }
        else
        {
            services.AddDbContext<NomadEventDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("EventsConnectionString");
                ArgumentNullException.ThrowIfNull(connectionString);
            });
        }
    }

    public static void AddInfraServices(this IServiceCollection services)
    {
        services.AddScoped<INomadEventRepository, NomadEventRepository>();
    }
}