using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Company : Participant, ICompany
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int ParticipantCount { get; set; }
        [StringLength(5000)]
        public string Description { get; set; }
    }
}
