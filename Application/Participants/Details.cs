using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Participants.Events
{
    public class Details
    {
        public class Query : IRequest<Result<Participant>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Participant>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Participant>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<Participant>.Success(await _context.Participants.FindAsync(request.Id));
            }
        }
    }
}
