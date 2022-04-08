using Application.Interfaces.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class QueryableExtensionsAbstraction : IQueryableExtensionsAbstraction
    {
        public IQueryable<T> ProjectTo<T>(
            IQueryable source,
            IConfigurationProvider configuration,
            params Expression<Func<T, object>>[] membersToExpand)
        {
            return source.ProjectTo<T>(configuration);
        }
    }
}
