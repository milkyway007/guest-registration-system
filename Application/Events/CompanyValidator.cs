using Application.Events;
using Domain;
using FluentValidation;
using Persistence;

namespace Application.Events
{
    public class CompanyValidator : ParticipantValidator<Company>
    {
        public CompanyValidator(DataContext dataContext) : base(dataContext)
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(5000);
        }
    }
}
