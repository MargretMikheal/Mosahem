using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;
using Mosahm.Domain.Entities.Location;

public class CityConfiguration
    : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.NameAr)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.NameEn)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasOne(c => c.Governorate)
               .WithMany(g => g.Cities)
               .HasForeignKey(c => c.GovernorateId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
