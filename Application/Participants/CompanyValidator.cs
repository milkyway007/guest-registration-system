using Domain;
using FluentValidation;
using Persistence;

namespace Application.Participants
{
    public class CompanyValidator : ParticipantValidator<Company>
    {
        public CompanyValidator(DataContext dataContext) : base(dataContext)
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).Must(UniqueName);
            RuleFor(x => x.Description).MaximumLength(5000);
        }

        private bool UniqueName(Company company, string name)
        {
            var dbParticipant = DataContext.Participants
                                .Where(x => x is Company)
                                .Cast<Company>()
                                .Where(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))
                                .SingleOrDefault();

            return dbParticipant == null;
        }
    }
}
