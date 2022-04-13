using static Constants.Enums;

namespace Domain.Entities
{
    public abstract class Participant : Entity
    {
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<EventParticipant> Events { get; set; }
        public string Code { get; set; }
    }
}
