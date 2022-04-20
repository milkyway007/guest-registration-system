using Application.Core;
using Application.Interfaces.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.Events.Queries
{
    public class List
    {
        public class Query : IRequest<Result<List<Event>>>
        {
            public string Predicate { get; set; }
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
                var query = _context.Events.AsQueryable();

                switch(request.Predicate)
                {
                    case "future":
                        query = query.Where(x => x.Occurrence > DateTime.Now).OrderBy(x => x.Occurrence);
                        break;
                    case "past":
                        query = query.Where(x => x.Occurrence <= DateTime.Now).OrderBy(x => x.Occurrence);
                        break;
                    default:
                        query = query.OrderBy(x => x.Occurrence);
                        break;
                }

                var queryWithFK = query.Include(x => x.Address)
                    .Include(x => x.Participants);

                var list = await _extensionsAbstraction.ToListAsync(queryWithFK, cancellationToken);

                return Result<List<Event>>.Success(list);
            }
        }
    }
}
