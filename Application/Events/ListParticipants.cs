using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

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
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<EventParticipantDto>>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var query = _context.EventParticipants
                    .Where(x => x.Event.Id == request.EventId)
                    .ProjectTo<EventParticipantDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                query = request.Predicate switch
                {
                    "person" => query.Where(x => !x.IsCompany).OrderBy(x => ((PersonDto)x.Participant).FirstName),
                    "company" => query.Where(x => x.IsCompany).OrderBy(x => ((CompanyDto)x.Participant).Name),
                    _ => query.OrderBy(x => x.Participant.Id)
                };

                var participants = await query.ToListAsync();

                return Result<List<EventParticipantDto>>.Success(participants);
            }
        }
    }
}
