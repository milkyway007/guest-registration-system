using Domain.Entities;
using FluentValidation;
using Persistence.Interfaces;

namespace Application.Events.Validators
{
    public class CompanyValidator : ParticipantValidator<Company>
    {
        public CompanyValidator(IDataContext dataContext) : base(dataContext)
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(5000);

            //RuleFor(x => x.Name).Must(UniqueName).When(x => x.Id == 0);
        }

        private bool UniqueName(string value)
        {
            var dbParticipant = DataContext.Participants.Where(x => x is Company).Cast<Company>()
                                .Where(x => x.Name.ToLower() == value.ToLower())
                                .SingleOrDefault();

            return dbParticipant == null;
        }
    }
}
