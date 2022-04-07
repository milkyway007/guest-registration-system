using Domain;
using FluentValidation;

namespace Application.Events
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
            RuleFor(c => c.Description).MaximumLength(1000);
        }
    }
}
