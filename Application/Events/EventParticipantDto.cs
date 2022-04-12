namespace Application.Events
{
    public class EventParticipantDto
    {
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public ParticipantDto Participant { get; set; }
        public bool IsPerson { get; set; }
    }
}
