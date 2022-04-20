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

            builder.HasKey(i => i.Code);
            builder.Property(b => b.Code).HasMaxLength(50);
            builder.Property(b => b.PaymentMethod).IsRequired().HasConversion<int>();
        }
    }
}
