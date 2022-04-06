using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPerson : IParticipant
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        object PaymentMethod { get; set; }

        string Code { get; set; }
        string Description { get; set; }
    }
}
