using Application.Core;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence.Interfaces;

namespace Application.Addresses.Commands
{
    public class Create
    {
        public class Command : IRequest<Result<int>>
        {
            public Address Address { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Address).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Result<int>> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                _context.Addresses.Add(request.Address);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<int>.Failure("Failed to create address.");
                }

                return Result<int>.Success(request.Address.Id);
            }
        }
    }
}
