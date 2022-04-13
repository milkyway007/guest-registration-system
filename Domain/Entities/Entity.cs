namespace Domain.Entities
{
    public abstract class Entity
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
