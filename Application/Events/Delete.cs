using Application.Core;
using MediatR;
using Persistence;
using Persistence.Interfaces;

namespace Application.Events
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
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
                    return Result<Unit>.Failure("Failed to delete the activity");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
