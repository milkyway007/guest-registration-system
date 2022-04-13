using Domain.Entities;
using FluentValidation;
using Persistence.Interfaces;

namespace Application.Events.Validators
{
    public class CompanyValidator : ParticipantValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(5000);
        }
    }
}
