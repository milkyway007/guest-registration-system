using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Event : Entity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime Occurrence { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public ICollection<EventParticipant> Participants { get; set; }
        [Required]
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
