using System.Security.Claims;

namespace NomadEvents.MinimalAPI.Infrastructure.Auth;

public interface IJwtService
{
    string GenerateJwt(List<Claim> claims);
}