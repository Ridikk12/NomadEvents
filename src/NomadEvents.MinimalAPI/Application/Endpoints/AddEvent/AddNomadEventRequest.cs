using NomadEvents.MinimalAPI.Domain.Entities;

namespace NomadEvents.MinimalAPI.Application.Endpoints;

public record AddNomadEventRequest(string Name, string Description, string CreatedBy, decimal Price, string Url,
    DateTime EventDate)
{
    public NomadEvent ToEntity()
    {
        return new NomadEvent()
        {
            Description = Description,
            Name = Name,
            Id = Guid.NewGuid(),
            Price = Price,
            MapsUrl = Url,
            CreatedDate = DateTime.Now,
            EventDate = EventDate
        };
    }
}