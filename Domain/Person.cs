using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person : Participant
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(1500)]
        public string Description { get; set; }
    }
}
