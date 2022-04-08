using Domain.Interfaces;

namespace Domain
{
    public class Event : Entity, IEvent
    {
        public string Name { get; set; }
        public DateTime Occurrence { get; set; }
        public string Description { get; set; }
        public ICollection<IEventParticipant> Participants { get; set; }
        public int AddressId { get; set; }
        public IAddress Address { get; set; }
    }
}
