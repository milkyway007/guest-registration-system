using Domain.Entities;
using FluentValidation;

namespace Application.Events.Validators
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.Occurrence).GreaterThan(DateTime.Now);
            RuleFor(x => x.AddressZip).MaximumLength(50);
        }
    }
}
