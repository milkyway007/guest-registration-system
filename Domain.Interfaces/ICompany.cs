namespace Domain.Interfaces
{
    public interface ICompany : IParticipant
    {
        string Name { get; set; }
        int ParticipantCount { get; set; }
        string Description { get; set; }
    }
}
