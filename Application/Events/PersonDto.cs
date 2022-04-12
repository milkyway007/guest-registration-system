using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events
{
    public class PersonDto : ParticipantDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
