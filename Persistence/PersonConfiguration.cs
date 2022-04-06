using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("persons");

            builder.Property(b => b.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.LastName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Description).HasMaxLength(1500);
        }
    }
}
