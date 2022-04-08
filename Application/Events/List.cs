using Application.Core;
using Application.Interfaces;
using Application.Interfaces.Core;
using Domain;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Interfaces;

namespace Application.Events
{
    public class List
    {
        public class Query : IRequest<Result<List<IEvent>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<IEvent>>>
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

            public async Task<Result<List<IEvent>>> Handle(
                Query request, CancellationToken cancellationToken)
            {
                return Result<List<IEvent>>.Success(
                    await _extensionsAbstraction.ToListAsync(_context.Events, cancellationToken));
            }
        }
    }
}
