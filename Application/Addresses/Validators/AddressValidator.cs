using Domain.Entities;
using FluentValidation;
using Persistence.Interfaces;

namespace Application.Addresses.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        private readonly IDataContext _dataContext;

        public AddressValidator(IDataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(x => x.Line1).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Line2).MaximumLength(250);
            RuleFor(x => x.City).NotEmpty().MaximumLength(50);
            RuleFor(x => x.State).MaximumLength(250);
            RuleFor(x => x.Zip).NotEmpty().MaximumLength(250).Must(UniqueZip);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(250);
        }

        private bool UniqueZip(string zip)
        {
            var dbAddress = _dataContext.Addresses.Where(x => x.Zip.ToLower() == zip.ToLower())
                                .SingleOrDefault();

            return dbAddress == null;
        }
    }
}
