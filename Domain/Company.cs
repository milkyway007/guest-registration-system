using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Company : Participant, ICompany
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public int ParticipantCount { get; set; }

        [Required]
        [StringLength(50)]
        public override string Code { get; set; }
        [StringLength(5000)]
        public override string Description { get; set; }
    }
}
