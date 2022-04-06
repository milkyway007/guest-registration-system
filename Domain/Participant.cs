using System.ComponentModel.DataAnnotations;
using static Domain.Enums;

namespace Domain
{
    public abstract class Participant : Entity
    {
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<EventParticipant> Events { get; set; }

        public abstract string Code { get; set; }
        public abstract string Description { get; set; }
    }
}
