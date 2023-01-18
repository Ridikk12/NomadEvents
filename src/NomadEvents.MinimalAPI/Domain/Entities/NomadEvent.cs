namespace NomadEvents.MinimalAPI.Domain.Entities
{
    public class NomadEvent : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<EventUser>? Users { get; set; }
        public string? MapsUrl { get; set; }
        public decimal? Price { get; set; }
        public string? Address { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
