using Domain.Entities;
using FluentValidation;
using Persistence;
using Persistence.Interfaces;

namespace Application.Events.Validators
{
    public class ParticipantValidator<T> : AbstractValidator<T> where T : Participant
    {
        protected readonly IDataContext DataContext;

        public ParticipantValidator(IDataContext dataContext)
        {
            DataContext = dataContext;

            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PaymentMethod).NotNull().IsInEnum();
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);

            //RuleFor(x => x.Code).Must(UniqueCode).When(x => x.Id == 0);
        }

        private bool UniqueCode(string code)
        {
            var dbParticipant = DataContext.Participants.Where(x => x.Code.ToLower() == code.ToLower())
                                .SingleOrDefault();

            return dbParticipant == null;
        }
    }
}
