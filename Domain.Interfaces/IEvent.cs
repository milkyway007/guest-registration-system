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
        IEnumerable<IParticipant> Participants { get; set; }
        int AddressId { get; set; }
    }
}
