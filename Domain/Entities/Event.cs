namespace Domain.Entities
{
    public class Event : Entity
    {
        public string Name { get; set; }
        public DateTime Occurrence { get; set; }
        public string Description { get; set; }
        public ICollection<EventParticipant> Participants { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
