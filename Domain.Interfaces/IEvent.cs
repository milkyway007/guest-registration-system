using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEvent
    {
        string Name { get; set; }
        DateTime Occurrence { get; set; }
        string Description { get; set; }
        ICollection<IEventParticipant> Participants { get; set; }
        IAddress Address { get; set; }
    }
}
