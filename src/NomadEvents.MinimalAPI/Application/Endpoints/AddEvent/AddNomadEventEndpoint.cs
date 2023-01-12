using FluentValidation;
using NomadEvents.MinimalAPI.Infrastructure;

namespace NomadEvents.MinimalAPI.Application.Endpoints;

public static class AddNomadEventEndpoint
{
    public static async Task<IResult> AddNomadEvent(IValidator<AddNomadEventRequest> validator, AddNomadEventRequest request, INomadEventRepository _nomadEventRepository,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }
        
        await _nomadEventRepository.Add(request.ToEntity());
        await _nomadEventRepository.SaveChangeAsync(cancellationToken);

        return Results.Ok();
    }
}