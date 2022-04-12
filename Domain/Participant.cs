using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Constants.Enums;

namespace Domain
{
    public class Participant : Entity
    {
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<EventParticipant> Events { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public bool IsPerson { get; set; }
    }
}
