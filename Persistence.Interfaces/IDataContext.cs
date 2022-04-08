using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.Interfaces
{
    public interface IDataContext
    {
        DbSet<IAddress> Addresses { get; set; }
        DbSet<IParticipant> Participants { get; set; }
        DbSet<IEvent> Events { get; set; }
        DbSet<IEventParticipant> EventParticipants { get; set; }

        Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync();
        EntityEntry Remove(object entity);
    }
}
