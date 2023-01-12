using NomadEvents.MinimalAPI.Domain.Entities;
using NomadEvents.MinimalAPI.Infrastructure;

namespace NomadEvents.MinimalAPI.Application.Endpoints.GetEvent;

public static class GetEventEndpoint
{
    public static async Task<IResult> GetNomadEvent(IRepository<NomadEvent> _repository, GetEventRequest request, CancellationToken cancellationToken)
    {
        var nomadEvent = await _repository.Get(x => x.Id == request.Id,cancellationToken);
        return nomadEvent is null ? Results.NotFound() : Results.Ok(GetEventResponse.ToGetEventResponse(nomadEvent));
    }
}