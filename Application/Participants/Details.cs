using Application.Core;
using Domain;
using Domain.Interfaces;
using MediatR;
using Persistence.Interfaces;

namespace Application.Participants
{
    public class Details
    {
        public class Query : IRequest<Result<Participant>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Participant>>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Result<Participant>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                return Result<Participant>.Success(await _context.Participants.FindAsync(request.Id));
            }
        }
    }
}
