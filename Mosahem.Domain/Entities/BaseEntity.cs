namespace mosahem.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
