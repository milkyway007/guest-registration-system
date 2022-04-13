using Application.Core;
using Application.Events.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence.Interfaces;

namespace Application.Participants.Commands
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Participant Participant { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                When(x => x.Participant is Person, () => {
                    RuleFor(x => (Person)x.Participant).SetValidator(new PersonValidator());
                }).Otherwise(() => {
                    RuleFor(x => (Company)x.Participant).SetValidator(new CompanyValidator());
                });
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var participant = await _context.Participants.FindAsync(request.Participant.Code);
                if (participant == null)
                {
                    return null;
                }

                _mapper.Map(request.Participant, participant);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return Result<Unit>.Failure("Failed to edit participant.");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
