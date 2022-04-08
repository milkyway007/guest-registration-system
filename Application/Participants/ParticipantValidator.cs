using Domain;
using FluentValidation;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Participants
{
    public class ParticipantValidator<T> : AbstractValidator<T> where T : Participant
    {
        protected readonly DataContext DataContext;
        public ParticipantValidator(DataContext dataContext)
        {
            DataContext = dataContext;

            RuleFor(x => x.Code).NotEmpty().MaximumLength(50).Must(UniqueCode);
            RuleFor(x => x.PaymentMethod).NotNull().IsInEnum();
        }

        private bool UniqueCode(Participant participant, string code)
        {
            var dbParticipant = DataContext.Participants
                                .Where(x => string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase))
                                .SingleOrDefault();

            return dbParticipant == null;
        }
    }
}
