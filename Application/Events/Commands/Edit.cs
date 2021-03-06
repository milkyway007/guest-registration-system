using Application.Addresses.Validators;
using Application.Core;
using Application.Events.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence.Interfaces;

namespace Application.Events.Commands
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Event Event { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Event).SetValidator(new EventValidator());
                RuleFor(x => x.Event.Address).SetValidator(new AddressValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var e = await _context.Events.FindAsync(request.Event.Id);
                if (e == null)
                {
                    return null;
                }

                _mapper.Map(request.Event, e);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to edit event.");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
