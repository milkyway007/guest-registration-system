namespace Domain.Interfaces
{
    public interface IEventParticipant
    {
        public IEvent Event { get; set; }
        public IParticipant Participant { get; set; }
    }
}
