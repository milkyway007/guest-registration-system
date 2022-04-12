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
        public class Query : IRequest<Result<List<PersonDto>>>
        {
            public int EventId { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.EventId).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<Query, Result<List<PersonDto>>>
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

            public async Task<Result<List<PersonDto>>> Handle(
                Query request, CancellationToken cancellationToken)
            {
                var personQuery = _context.EventParticipants.Where(x => x.Event.Id == request.EventId)
                    .Include(x => x.Participant).Select(x => x.Participant).OfType<Person>();

                var personDtoQuery = _extensionsAbstraction.ProjectTo<PersonDto>(
                    personQuery, _mapper.ConfigurationProvider).AsQueryable().OrderBy(x => x.FirstName);

                return Result<List<PersonDto>>.Success
                    (
                        await _eFextensionsAbstraction.ToListAsync(personDtoQuery, cancellationToken)
                    );
            }
        }
    }
}
