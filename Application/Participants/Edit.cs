using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence.Interfaces;

namespace Participants.Events
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Participant Participant { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Participant).NotEmpty();
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
                var participant = await _context.Participants.FindAsync(request.Participant.Id);
                if (participant == null)
                {
                    return null;
                }

                _mapper.Map(request.Participant, participant);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to edit participant.");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
