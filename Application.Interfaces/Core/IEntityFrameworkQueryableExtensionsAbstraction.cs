using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Core
{
    public interface IEntityFrameworkQueryableExtensionsAbstraction
    {
        Task<List<T>> ToListAsync<T>(
            IQueryable<T> target,
            CancellationToken cancellationToken = default);

        Task<T> AddAsync<T>(T entity, DbSet<T> dataSet) where T : class;
    }
}
