using Domain;
using FluentValidation;
using Persistence;

namespace Application.Participants
{
    public class CompanyEditValidator : ParticipantEditValidator<Company>
    {
        public CompanyEditValidator(DataContext dataContext) : base(dataContext)
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(5000);
        }
    }
}
