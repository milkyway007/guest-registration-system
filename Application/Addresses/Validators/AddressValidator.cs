using Domain.Entities;
using FluentValidation;

namespace Application.Addresses.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Line1).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Line2).MaximumLength(250);
            RuleFor(x => x.City).NotEmpty().MaximumLength(50);
            RuleFor(x => x.State).MaximumLength(250);
            RuleFor(x => x.Zip).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(250);
        }
    }
}
