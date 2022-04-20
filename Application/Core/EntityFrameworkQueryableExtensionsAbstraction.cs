using Application.Interfaces.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.Core
{
    public class EntityFrameworkQueryableExtensionsAbstraction :
        IEntityFrameworkQueryableExtensionsAbstraction
    {
        public async Task<T> AddAsync<T>(T entity, DbSet<T> dataSet) where T : class
        {
            return (await dataSet.AddAsync(entity)).Entity;
        }

        public Task<List<T>> ToListAsync<T>(
            IQueryable<T> target,
            CancellationToken cancellationToken = default)
        {
            return target.ToListAsync(cancellationToken);
        }
    }
}
