using Domain.Entities;
using FluentValidation;
using Persistence;
using Persistence.Interfaces;

namespace Application.Events.Validators
{
    public class ParticipantValidator<T> : AbstractValidator<T> where T : Participant
    {
        public ParticipantValidator()
        {
            RuleFor(x => x.Code).MaximumLength(50);
            RuleFor(x => x.PaymentMethod).NotNull().IsInEnum();
        }
    }
}
