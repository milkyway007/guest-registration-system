namespace Domain.Interfaces
{
    public interface ICompany : IParticipant
    {
        string Name { get; set; }
        int ParticipantCount { get; set; }
        object PaymentMethod { get; set; }

        string Code { get; set; }
        string Description { get; set; }
    }
}
