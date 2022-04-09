using Application.Core;
using Application.Interfaces.Core;
using AutoMapper;
using MediatR;
using Persistence.Interfaces;

namespace Application.Events
{
    public class ListParticipants
    {
        public class Query : IRequest<Result<List<EventParticipantDto>>>
        {
            public int EventId { get; set; }
            public string Predicate { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<EventParticipantDto>>>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;
            private readonly IEntityFrameworkQueryableExtensionsAbstraction _eFextensionsAbstraction;
            private readonly IQueryableExtensionsAbstraction _extensionsAbstraction;

            public Handler(
                IDataContext context,
                IMapper mapper,
                IEntityFrameworkQueryableExtensionsAbstraction eFextensionsAbstraction,
                IQueryableExtensionsAbstraction extensionsAbstraction)
            {
                _mapper = mapper;
                _context = context;
                _eFextensionsAbstraction = eFextensionsAbstraction;
                _extensionsAbstraction = extensionsAbstraction;
            }

            public async Task<Result<List<EventParticipantDto>>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var eventParticipantQuery = _context.EventParticipants
                    .Where(x => x.Event.Id == request.EventId);
                var eventParticipantDtoQuery = _extensionsAbstraction.ProjectTo<EventParticipantDto>(
                    eventParticipantQuery, _mapper.ConfigurationProvider) 
                    .AsQueryable();
                eventParticipantDtoQuery = request.Predicate switch
                {
                    "person" => 
                    eventParticipantDtoQuery.Where(x => !x.IsCompany).OrderBy(x => ((PersonDto)x.Participant).FirstName),
                    "company" => 
                    eventParticipantDtoQuery.Where(x => x.IsCompany).OrderBy(x => ((CompanyDto)x.Participant).Name),
                    _ => eventParticipantDtoQuery.OrderBy(x => x.Participant.Id)
                };

                var participants = await _eFextensionsAbstraction
                    .ToListAsync(eventParticipantDtoQuery, cancellationToken);

                return Result<List<EventParticipantDto>>.Success(participants);
            }
        }
    }
}
