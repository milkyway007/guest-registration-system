using Application.Addresses.Validators;
using Application.Core;
using Application.Events.Dtos;
using Application.Events.Validators;
using Application.Interfaces.Core;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence.Interfaces;

namespace Application.Events.Commands
{
    public class Create
    {
        public class Command : IRequest<Result<Event>>
        {
            public EventDto Event { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Event).SetValidator(new EventDtoValidator());
                RuleFor(x => x.Event.Address).SetValidator(new AddressValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Event>>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;
            private readonly IEntityFrameworkQueryableExtensionsAbstraction _eFExtensionsAbstraction;

            public Handler(
                IDataContext context,
                IMapper mapper,
                IEntityFrameworkQueryableExtensionsAbstraction eFExtensionsAbstraction)
            {
                _context = context;
                _mapper = mapper;
                _eFExtensionsAbstraction = eFExtensionsAbstraction;
            }

            public async Task<Result<Event>> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var address = await _context.Addresses.FindAsync(request.Event.AddressZip);
                if (address == null)
                {
                    address = await _eFExtensionsAbstraction.AddAsync(request.Event.Address, _context.Addresses);
                }

                var mappedEvent = new Event();
                _mapper.Map(request.Event, mappedEvent);
                mappedEvent.AddressZip = address.Zip;
                await _eFExtensionsAbstraction.AddAsync(mappedEvent, _context.Events);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<Event>.Failure("Failed to create event.");
                }

                return Result<Event>.Success(mappedEvent);
            }
        }
    }
}
