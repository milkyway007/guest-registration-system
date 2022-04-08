namespace Application.Interfaces.Core
{
    public interface IEntityFrameworkQueryableExtensionsAbstraction
    {
        Task<List<T>> ToListAsync<T>(
            IQueryable<T> target,
            CancellationToken cancellationToken = default);
    }
}
