using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events");

            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Occurrence);
            builder.Property(b => b.Description).HasMaxLength(1000);

            builder
                .HasOne(ev => ev.Address)
                .WithMany(address => address.Events)
                .HasForeignKey(ev => ev.AddressId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
