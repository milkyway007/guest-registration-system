using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.Events.Commands
{
    public class CancelParticipation
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int EventId { get; set; }
            public string ParticipantCode { get; set; }
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

                var participant = e.Participants.FirstOrDefault(x => x.Participant.Code == request.ParticipantCode);
                if (participant == null)
                {
                    return null;
                }

                e.Participants.Remove(participant);
                var result = await _context.SaveChangesAsync() > 0;

                return result ?
                    Result<Unit>.Success(Unit.Value) :
                    Result<Unit>.Failure("Problem canceling participation.");
            }
        }
    }
}
