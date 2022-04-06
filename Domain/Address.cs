using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Address : Entity, IAddress
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
    }
}
