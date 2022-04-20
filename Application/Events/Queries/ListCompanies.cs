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
    public class ListCompanies
    {
        public class Query : IRequest<Result<CompanyListDto>>
        {
            public int EventId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<CompanyListDto>>
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

            public async Task<Result<CompanyListDto>> Handle(
                Query request, CancellationToken cancellationToken)
            {
                var e = await _context.Events.FindAsync(request.EventId);
                if (e == null)
                {
                    return null;
                }

                var companyQuery = _context.EventParticipants.Where(x => x.Event.Id == request.EventId)
                    .Include(x => x.Participant)
                    .Select(x => x.Participant).OfType<Company>();

                var companyDtoQuery = _extensionsAbstraction.ProjectTo<CompanyDto>(
                    companyQuery, _mapper.ConfigurationProvider).OrderBy(x => x.Name);

                var companyDtoList = await _eFextensionsAbstraction
                        .ToListAsync(companyDtoQuery, cancellationToken);

                companyDtoList.ForEach(x => x.EventId = request.EventId);

                return Result<CompanyListDto>.Success
                    (
                        new CompanyListDto
                        {
                            EventName = e.Name,
                            Participants = companyDtoList,
                        }
                    );
            }
        }
    }
}
