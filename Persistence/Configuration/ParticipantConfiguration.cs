using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable(Constants.PARTICIPANTS);

            builder.Property(b => b.Code).IsRequired().HasMaxLength(50);
            builder.Property(b => b.PaymentMethod).IsRequired().HasConversion<int>();

            builder.HasIndex(u => u.Code).IsUnique();
        }
    }
}
