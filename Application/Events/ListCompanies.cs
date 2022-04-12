using Application.Core;
using Application.Interfaces.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events
{
    public class ListCompanies
    {
        public class Query : IRequest<Result<List<CompanyDto>>>
        {
            public int EventId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<CompanyDto>>>
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

            public async Task<Result<List<CompanyDto>>> Handle(
                Query request, CancellationToken cancellationToken)
            {
                var companyQuery = _context.EventParticipants
                    .Where(x => x.Event.Id == request.EventId)
                    .Include(x => x.Participant)
                    .Select(x => x.Participant)
                    .OfType<Company>();
                    
                var companyDtoQuery = _extensionsAbstraction.ProjectTo<CompanyDto>(
                    companyQuery, _mapper.ConfigurationProvider)
                    .AsQueryable().OrderBy(x => x.Name);

                return Result<List<CompanyDto>>.Success
                    (
                        await _eFextensionsAbstraction.ToListAsync(companyDtoQuery, cancellationToken)
                    );
            }
        }
    }
}
