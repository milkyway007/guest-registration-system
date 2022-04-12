using Domain;
using FluentValidation;
using Persistence;

namespace Application.Events
{
    public class ParticipantValidator<T> : AbstractValidator<T> where T : Participant
    {
        protected readonly DataContext DataContext;

        public ParticipantValidator(DataContext dataContext)
        {
            DataContext = dataContext;

            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PaymentMethod).NotNull().IsInEnum();
        }
    }
}
