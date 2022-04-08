using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events
{
    public class EventParticipantResultDto
    {
        public ICollection<ParticipantDto> Companies { get; set; }
        public ICollection<ParticipantDto> Persons { get; set; }
    }
}
