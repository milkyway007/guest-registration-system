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
                var query = _context.EventParticipants
                    .Where(x => x.Event.Id == request.EventId);

                var query2 = _extensionsAbstraction.ProjectTo<EventParticipantDto>(
                    query, _mapper.ConfigurationProvider) 
                    .AsQueryable();

                query2 = request.Predicate switch
                {
                    "person" => 
                    query2.Where(x => !x.IsCompany).OrderBy(x => ((PersonDto)x.Participant).FirstName),
                    "company" => 
                    query2.Where(x => x.IsCompany).OrderBy(x => ((CompanyDto)x.Participant).Name),
                    _ => query2.OrderBy(x => x.Participant.Id)
                };

                var participants = await _eFextensionsAbstraction.ToListAsync(query2, cancellationToken);

                return Result<List<EventParticipantDto>>.Success(participants);
            }
        }
    }
}
