using Application.Core;
using Application.Events.Dtos;
using Application.Interfaces.Core;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Application.Events.Queries
{
    public class ListPersons
    {
        public class Query : IRequest<Result<PersonListDto>>
        {
            public int EventId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PersonListDto>>
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

            public async Task<Result<PersonListDto>> Handle(
                Query request, CancellationToken cancellationToken)
            {
                var e = await _context.Events.FindAsync(request.EventId);
                if (e == null)
                {
                    return null;
                }

                var personQuery = _context.EventParticipants.Where(x => x.Event.Id == request.EventId)
                    .Include(x => x.Participant).Select(x => x.Participant).OfType<Person>();

                var personDtoQuery = _extensionsAbstraction.ProjectTo<PersonDto>(
                    personQuery, _mapper.ConfigurationProvider).AsQueryable().OrderBy(x => x.FirstName);

                var personDtoList = await _eFextensionsAbstraction
                        .ToListAsync(personDtoQuery, cancellationToken);

                personDtoList.ForEach(x => x.EventId = request.EventId);

                return Result<PersonListDto>.Success
                    (
                        new PersonListDto
                        {
                            EventName = e.Name,
                            Participants = personDtoList,
                        }
                    );
            }
        }
    }
}
