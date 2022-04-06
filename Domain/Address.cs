using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Address : Entity
    {
        [Required]
        [StringLength(250)]
        public string Line1 { get; set; }
        [StringLength(250)]
        public string Line2 { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        [Required]
        [StringLength(50)]
        public string Zip { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
