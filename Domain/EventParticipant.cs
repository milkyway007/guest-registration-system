using Domain.Interfaces;

namespace Domain
{
    public class EventParticipant
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }
        public bool IsPerson { get; set; }
    }
}
