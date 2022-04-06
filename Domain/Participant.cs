using System.ComponentModel.DataAnnotations;
using static Domain.Enums;

namespace Domain
{
    public abstract class Participant : Entity
    {
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<EventParticipant> Events { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
    }
}
