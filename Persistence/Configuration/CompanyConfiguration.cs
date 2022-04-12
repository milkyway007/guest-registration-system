using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(Constants.COMPANIES);

            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.ParticipantCount);
            builder.Property(b => b.Description).HasMaxLength(5000);


            builder.HasIndex(u => u.Name).IsUnique();
        }
    }
}
