namespace Domain.Interfaces
{
    public interface IParticipant
    {
        ICollection<IEventParticipant> Events { get; set; }
    }
}
