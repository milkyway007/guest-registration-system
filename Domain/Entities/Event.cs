namespace Domain.Entities
{
    public class Event : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Occurrence { get; set; }
        public string Description { get; set; }
        public ICollection<EventParticipant> Participants { get; set; }
        public string AddressZip { get; set; }
        public Address Address { get; set; }
    }
}
