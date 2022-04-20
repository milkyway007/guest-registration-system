using static Constants.Enums;

namespace Application.Events.Dtos
{
    public abstract class ParticipantDto
    {
        public int EventId { get; set; }
        public string Code { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
