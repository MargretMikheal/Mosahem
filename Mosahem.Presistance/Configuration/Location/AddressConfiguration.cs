using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Location;

public class AddressConfiguration
    : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Description)
               .HasMaxLength(500);

        builder.HasOne(a => a.City)
               .WithMany(c => c.Addresses)
               .HasForeignKey(a => a.CityId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => a.CityId);
    }
}
