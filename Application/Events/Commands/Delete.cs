using Application.Core;
using FluentValidation;
using MediatR;
using Persistence.Interfaces;

namespace Application.Events.Commands
{
    public class Delete
    {
        public class Command : IRequest<Result<int>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                var e = await _context.Events.FindAsync(request.Id);
                if(e == null)
                {
                    return null;
                }

                _context.Remove(e);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<int>.Failure("Failed to delete activity");
                }

                return Result<int>.Success(e.Id);
            }
        }
    }
}
