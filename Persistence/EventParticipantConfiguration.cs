using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    public class EventParticipantConfiguration : IEntityTypeConfiguration<IEventParticipant>
    {
        public void Configure(EntityTypeBuilder<IEventParticipant> builder)
        {
            builder.ToTable("event_participants");

            builder.HasKey(x => new {x.EventId, x.ParticipantId});

            builder
                .HasOne(ep => ep.Event)
                .WithMany(ev => ev.Participants)
                .HasForeignKey(ev => ev.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(ep => ep.Participant)
                .WithMany(participant => participant.Events)
                .HasForeignKey(ev => ev.ParticipantId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
