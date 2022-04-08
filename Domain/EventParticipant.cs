using Domain.Interfaces;

namespace Domain
{
    public class EventParticipant : IEventParticipant
    {
        public int EventId { get; set; }
        public IEvent Event { get; set; }
        public int ParticipantId { get; set; }
        public IParticipant Participant { get; set; }
    }
}
