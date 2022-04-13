using Application.Addresses.Validators;
using Application.Core;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence.Interfaces;

namespace Application.Addresses.Commands
{
    public class Create
    {
        public class Command : IRequest<Result<string>>
        {
            public Address Address { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Address).SetValidator(new AddressValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context)
            {
                _context = context;
            }

            public async Task<Result<string>> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                _context.Addresses.Add(request.Address);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<string>.Failure("Failed to create address.");
                }

                return Result<string>.Success(request.Address.Zip);
            }
        }
    }
}
