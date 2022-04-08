using static Constants.Enums;

namespace Domain.Interfaces
{
    public interface IParticipant
    {
        int Id { get; set; }
        string Code { get; set; }
        ICollection<IEventParticipant> Events { get; set; }
        PaymentMethod PaymentMethod { get; set; }
    }
}
