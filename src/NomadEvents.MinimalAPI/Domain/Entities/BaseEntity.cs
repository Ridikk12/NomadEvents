namespace NomadEvents.MinimalAPI.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
