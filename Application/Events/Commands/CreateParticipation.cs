using Application.Core;
using Application.Events.Validators;
using Application.Interfaces.Core;
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
            public CommandValidator()
            {
                When(x => x.Participant is Person, () => {
                    RuleFor(x => (Person)x.Participant).SetValidator(new PersonValidator());
                }).Otherwise(() => {
                    RuleFor(x => (Company)x.Participant).SetValidator(new CompanyValidator());
                });
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IDataContext _context;
            private readonly IEntityFrameworkQueryableExtensionsAbstraction _eFExtensionsAbstraction;

            public Handler(
                IDataContext context,
                IEntityFrameworkQueryableExtensionsAbstraction eFExtensionsAbstraction)
            {
                _context = context;
                _eFExtensionsAbstraction = eFExtensionsAbstraction;
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

                var eventParticipant = e.Participants
                    .FirstOrDefault(x => x.Participant.Code == request.Participant.Code);
                if (eventParticipant != null)
                {
                    return Result<Unit>
                        .Failure("Participant with same code has already been registered to event.");
                }

                var participant = await _context.Participants.FindAsync(request.Participant.Code);
                if (participant == null)
                {
                    participant = await _eFExtensionsAbstraction.AddAsync(request.Participant, _context.Participants);
                }

                var newEventParticipant = new EventParticipant
                {
                    EventId = e.Id,
                    ParticipantCode = request.Participant.Code,
                };

                e.Participants.Add(newEventParticipant);
                var result = await _context.SaveChangesAsync() > 0;

                return result ?
                    Result<Unit>.Success(Unit.Value) :
                    Result<Unit>.Failure("Problem registering user to event.");
            }
        }
    }
}
