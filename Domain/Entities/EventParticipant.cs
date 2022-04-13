namespace Domain.Entities
{
    public class EventParticipant
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string ParticipantCode { get; set; }
        public Participant Participant { get; set; }
    }
}
