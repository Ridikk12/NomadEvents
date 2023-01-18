using System.Security.Claims;
using NomadEvents.MinimalAPI.Infrastructure;
using NomadEvents.MinimalAPI.Infrastructure.Auth;

namespace NomadEvents.MinimalAPI.Application.Endpoints.GetEvent;

public static class LoginEndpoint
{
    public static IResult Login(LoginRequest request, IJwtService jwtService)
    {
        //Temporary login method
        if (!(request.Login == "test135" && request.Password == "test135"))
        {
            return Results.BadRequest(new ErrorMessage(-1, "Ups. Not valid credentials"));
        }

        var userClaim = new Claim(ClaimTypes.Name, "ridikk12");
        var emailClaim = new Claim(ClaimTypes.Email, "user@example.com");

        var token = jwtService.GenerateJwt(new List<Claim>() { userClaim, emailClaim });
        return Results.Ok(new LoginResponse(token));
    }
}