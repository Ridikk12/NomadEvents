using NomadEvents.MinimalAPI.Domain.Entities;

namespace NomadEvents.MinimalAPI.Application.Endpoints.GetEvent;

public class GetEventResponse
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid Id { get; set; }
    public string? EventDate { get; set; }
    public string? Address { get; set; }
    public string? Url { get; set; }

    public static GetEventResponse ToGetEventResponse(NomadEvent @event)
    {
        return new GetEventResponse()
        {
            Address = @event.Address,
            Description = @event.Description,
            Id = @event.Id,
            Url = @event.Url,
            EventDate = @event.Url,
            Name = @event.Name
        };
    }
}