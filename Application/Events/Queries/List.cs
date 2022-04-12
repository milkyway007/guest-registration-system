using Application.Core;
using Application.Interfaces.Core;
using Domain.Entities;
using MediatR;
using Persistence.Interfaces;

namespace Application.Events.Queries
{
    public class List
    {
        public class Query : IRequest<Result<List<Event>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<Event>>>
        {
            private readonly IDataContext _context;
            private readonly IEntityFrameworkQueryableExtensionsAbstraction _extensionsAbstraction;

            public Handler(
                IDataContext context,
                IEntityFrameworkQueryableExtensionsAbstraction extensionsAbstraction)
            {
                _context = context;
                _extensionsAbstraction = extensionsAbstraction;
            }

            public async Task<Result<List<Event>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var list = await _extensionsAbstraction.ToListAsync(_context.Events, cancellationToken);

                return Result<List<Event>>.Success(list);
            }
        }
    }
}
