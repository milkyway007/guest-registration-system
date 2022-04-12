using Application.Core;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.Events.Commands
{
    public class CreateParticipation
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int EventId { get; set; }
            public Participant Participant { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IDataContext _context;

            public CommandValidator(IDataContext context)
            {
                _context = context;

                RuleFor(x => x.Participant.Code).Must(UniqueCode);
                RuleFor(x => ((Company)x.Participant).Name).Must(UniqueName).When(x => x.Participant is Company);
            }

            private bool UniqueCode(string code)
            {
                var dbParticipant = _context.Participants.Where(x => x.Code.ToLower() == code.ToLower())
                                    .SingleOrDefault();

                return dbParticipant == null;
            }

            private bool UniqueName(string value)
            {
                var dbParticipant = _context.Participants.Where(x => x is Company).Cast<Company>()
                                    .Where(x => x.Name.ToLower() == value.ToLower())
                                    .SingleOrDefault();

                return dbParticipant == null;
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var e = await _context.Events.Include(x => x.Participants).ThenInclude(x => x.Participant)
                    .SingleOrDefaultAsync(x => x.Id == request.EventId);
                if (e == null)
                {
                    return null;
                }

                var participant = e.Participants
                    .FirstOrDefault(x => x.Participant.Code == request.Participant.Code);
                if (participant != null)
                {
                    return Result<Unit>
                        .Failure("Participant with same code has already been registered to event.");
                }

                var eventParticipant = new EventParticipant
                {
                    Event = e,
                    Participant = request.Participant,
                };

                _context.Participants.Add(request.Participant);
                e.Participants.Add(eventParticipant);
                var result = await _context.SaveChangesAsync() > 0;

                return result ?
                    Result<Unit>.Success(Unit.Value) :
                    Result<Unit>.Failure("Problem registering user to event.");
            }
        }
    }
}
