using Application.Events.Dtos;
using FluentValidation;
using System.Globalization;

namespace Application.Events.Validators
{
    public class EventDtoValidator : AbstractValidator<EventDto>
    {
        public EventDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => 
            DateTime.ParseExact(x.Occurrence, Constants.EVENT_DTO_DATE_FORMAT, CultureInfo.InvariantCulture))
                .GreaterThan(DateTime.Now);
            RuleFor(x => x.AddressZip).MaximumLength(50);
        }
    }
}
