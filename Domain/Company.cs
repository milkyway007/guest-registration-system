using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Company : Participant
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int ParticipantCount { get; set; }

        [Required]
        [StringLength(50)]
        public override string Code { get; set; }
        [StringLength(5000)]
        public override string Description { get; set; }
    }
}
