using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.ToTable(Constants.EVENT_PARTICIPANTS);

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
