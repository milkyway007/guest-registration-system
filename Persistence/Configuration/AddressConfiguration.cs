using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(Constants.ADDRESSES);

            builder.HasKey(i => i.Zip);
            builder.Property(b => b.Line1).IsRequired().HasMaxLength(250);
            builder.Property(b => b.Line2).HasMaxLength(250);
            builder.Property(b => b.City).IsRequired().HasMaxLength(50);
            builder.Property(b => b.State).HasMaxLength(250);
            builder.Property(b => b.Zip).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Country).IsRequired().HasMaxLength(50);

            builder
                .HasMany(address => address.Events)
                .WithOne(ev => ev.Address)
                .HasForeignKey(ev => ev.AddressZip);
        }
    }
}
