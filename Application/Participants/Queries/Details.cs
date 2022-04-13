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
            public string Code { get; set; }
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
                return Result<Participant>.Success(await _context.Participants.FindAsync(request.Code));
            }
        }
    }
}
