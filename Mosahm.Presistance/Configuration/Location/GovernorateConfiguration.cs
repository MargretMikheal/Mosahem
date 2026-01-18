using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;
using Mosahm.Domain.Entities.Location;

public class GovernorateConfiguration
    : IEntityTypeConfiguration<Governorate>
{
    public void Configure(EntityTypeBuilder<Governorate> builder)
    {
        builder.ToTable("Governorates");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.NameAr)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(g => g.NameEn)
               .IsRequired()
               .HasMaxLength(200);
    }
}
