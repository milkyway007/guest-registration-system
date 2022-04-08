using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    public class CompanyConfiguration : IEntityTypeConfiguration<ICompany>
    {
        public void Configure(EntityTypeBuilder<ICompany> builder)
        {
            builder.ToTable("companies");

            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.ParticipantCount);
            builder.Property(b => b.Description).HasMaxLength(5000);


            builder.HasIndex(u => u.Name).IsUnique();
        }
    }
}
