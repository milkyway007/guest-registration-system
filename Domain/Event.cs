using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Event : Entity, IEvent
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime Occurrence { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public IEnumerable<IParticipant> Participants { get; set; }

        [Range(0, int.MaxValue)]
        public int AddressId { get; set; }
    }
}
