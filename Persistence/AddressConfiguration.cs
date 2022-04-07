using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("addresses");

            builder.Property(b => b.Line1).IsRequired().HasMaxLength(250);
            builder.Property(b => b.Line2).HasMaxLength(250);
            builder.Property(b => b.City).IsRequired().HasMaxLength(50);
            builder.Property(b => b.State).HasMaxLength(250);
            builder.Property(b => b.Zip).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Country).IsRequired().HasMaxLength(50);

            builder.HasIndex(u => u.Zip).IsUnique();

            builder
                .HasMany(address => address.Events)
                .WithOne(ev => ev.Address)
                .HasForeignKey(ev => ev.AddressId);
        }
    }
}
