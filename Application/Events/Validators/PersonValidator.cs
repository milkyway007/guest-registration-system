using Domain.Entities;
using FluentValidation;

namespace Application.Events.Validators
{
    public class PersonValidator : ParticipantValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(1500);
        }
    }
}
