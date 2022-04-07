using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("participant");

            builder.Property(b => b.Code).IsRequired().HasMaxLength(50);
            builder.Property(b => b.PaymentMethod).IsRequired().HasConversion<int>();

            builder.HasIndex(u => u.Code).IsUnique();
        }
    }
}
