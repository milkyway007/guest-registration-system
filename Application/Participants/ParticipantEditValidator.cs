using Domain;
using FluentValidation;
using Persistence;

namespace Application.Participants
{
    public class ParticipantEditValidator<T> : AbstractValidator<T> where T : Participant
    {
        protected readonly DataContext DataContext;
        public ParticipantEditValidator(DataContext dataContext)
        {
            DataContext = dataContext;

            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PaymentMethod).NotNull().IsInEnum();
        }
    }
}
