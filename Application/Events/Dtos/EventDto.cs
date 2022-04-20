using Domain.Entities;

namespace Application.Events.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Occurrence { get; set; }
        public string Description { get; set; }
        public ICollection<EventParticipant> Participants { get; set; }
        public string AddressZip { get; set; }
        public Address Address { get; set; }
    }
}
