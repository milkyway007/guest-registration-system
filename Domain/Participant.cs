using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using static Constants.Enums;

namespace Domain
{
    public abstract class Participant : Entity, IParticipant
    {
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<IEventParticipant> Events { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
    }
}
