using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.Events
{
    public class CreateParticipation
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int EventId { get; set; }
            public Participant Participant { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IDataContext DataContext;

            public CommandValidator(IDataContext dataContext)
            {
                DataContext = dataContext;

                RuleFor(x => x.Participant).NotEmpty();
                RuleFor(x => x.Participant.Code).NotEmpty().MaximumLength(50).Must(UniqueCode);
                RuleFor(x => x.Participant).Must(UniqueName);
            }

            private bool UniqueCode(string code)
            {
                var dbParticipant = DataContext.Participants
                                    .Where(x => x.Code.ToLower() == code.ToLower())
                                    .SingleOrDefault();

                return dbParticipant == null;
            }

            private bool UniqueName(Participant participant)
            {
                if (participant is Person)
                {
                    return true;
                }

                var dbParticipant = DataContext.Participants
                                    .Where(x => x is Company)
                                    .Cast<Company>()
                                    .Where(x => x.Name.ToLower() == ((Company)participant).Name.ToLower())
                                    .SingleOrDefault();

                return dbParticipant == null;
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(
                IDataContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var e = await _context.Events
                    .Include(x => x.Participants)
                    .ThenInclude(x => x.Participant)
                    .SingleOrDefaultAsync(x => x.Id == request.EventId);
                if (e == null)
                {
                    return null;
                }

                var participant = e.Participants
                    .FirstOrDefault(x => x.Participant.Code == request.Participant.Code);
                if (participant != null)
                {
                    return Result<Unit>
                        .Failure("Participant with same code has already been registered to event.");
                }

                var eventParticipant = new EventParticipant
                {
                    Event = e,
                    Participant = request.Participant,
                };

                _context.Participants.Add(request.Participant);
                e.Participants.Add(eventParticipant);
                var result = await _context.SaveChangesAsync() > 0;

                return result ?
                    Result<Unit>.Success(Unit.Value) :
                    Result<Unit>.Failure("Problem registering user to event.");
            }
        }
    }
}
