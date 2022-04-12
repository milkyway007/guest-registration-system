using Application.Interfaces.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.Core
{
    public class EntityFrameworkQueryableExtensionsAbstraction :
        IEntityFrameworkQueryableExtensionsAbstraction
    {
        public Task<List<T>> ToListAsync<T>(
            IQueryable<T> target,
            CancellationToken cancellationToken = default)
        {
            return target.ToListAsync(cancellationToken);
        }
    }
}
