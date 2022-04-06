using System.ComponentModel.DataAnnotations;
using static Domain.Enums;

namespace Domain
{
    public abstract class Participant : Entity
    {
        [Required]
        [EnumDataType(typeof(PaymentMethod))]
        public object PaymentMethod { get; set; }

        public abstract string Code { get; set; }
        public abstract string Description { get; set; }
    }
}
