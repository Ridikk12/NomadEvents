using System.Security.Claims;

namespace NomadEvents.MinimalAPI.Application.Endpoints.LoginDetails;

public class LoginDetailsEndpoint
{
    public static IResult GetLoginDetails(ClaimsPrincipal claimsPrincipal)
    {
        return Results.Ok(new LoginDetailsResponse(claimsPrincipal.Identity.Name));
    }
}