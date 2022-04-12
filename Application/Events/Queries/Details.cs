using Application.Core;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence.Interfaces;

namespace Application.Events.Queries
{
    public class Details
    {
        public class Query : IRequest<Result<Event>>
        {
            public int Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<Query, Result<Event>>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Result<Event>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                return Result<Event>.Success(await _context.Events.FindAsync(request.Id));
            }
        }
    }
}
