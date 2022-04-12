using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.Interfaces
{
    public interface IDataContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<Participant> Participants { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<EventParticipant> EventParticipants { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Remove(object entity);
    }
}
