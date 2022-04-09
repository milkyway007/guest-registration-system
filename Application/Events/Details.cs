using Application.Core;
using Domain.Interfaces;
using MediatR;
using Persistence.Interfaces;

namespace Application.Events
{
    public class Details
    {
        public class Query : IRequest<Result<IEvent>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<IEvent>>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Result<IEvent>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<IEvent>.Success(await _context.Events.FindAsync(request.Id));
            }
        }
    }
}
