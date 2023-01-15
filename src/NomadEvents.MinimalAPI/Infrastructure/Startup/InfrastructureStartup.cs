using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NomadEvents.MinimalAPI.Infrastructure.Auth;

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

    public static void AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
            var issuer = configuration["Jwt:Issuer"];
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidAudiences = new List<string>() { issuer },
                ValidateAudience = true,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        });

        services.AddAuthorization();
    }
}