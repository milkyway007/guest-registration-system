using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEvent
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime Occurrence { get; set; }
        string Description { get; set; }
        ICollection<IEventParticipant> Participants { get; set; }
        int AddressId { get; set; }
        IAddress Address { get; set; }
    }
}
