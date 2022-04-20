using Domain.Entities;
using FluentValidation;

namespace Application.Events.Validators
{
    public class CompanyValidator : ParticipantValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(5000);
            RuleFor(x => x.ParticipantCount).GreaterThanOrEqualTo(1);
        }
    }
}
