using Application.Core;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence.Interfaces;

namespace Application.Participants.Queries
{
    public class Details
    {
        public class Query : IRequest<Result<Participant>>
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
