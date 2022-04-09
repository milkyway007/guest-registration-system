using Application.Core;
using Domain.Interfaces;
using MediatR;
using Persistence.Interfaces;

namespace Participants.Events
{
    public class Details
    {
        public class Query : IRequest<Result<IParticipant>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<IParticipant>>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Result<IParticipant>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                return Result<IParticipant>.Success(await _context.Participants.FindAsync(request.Id));
            }
        }
    }
}
