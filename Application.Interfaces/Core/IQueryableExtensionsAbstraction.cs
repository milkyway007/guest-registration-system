using AutoMapper;
using System.Linq.Expressions;

namespace Application.Interfaces.Core
{
    public interface IQueryableExtensionsAbstraction
    {
        IQueryable<T> ProjectTo<T>(
            IQueryable source,
            IConfigurationProvider configuration,
            params Expression<Func<T, object>>[] membersToExpand);
    }
}
