using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities.MasterData;

public class FieldConfiguration
    : IEntityTypeConfiguration<Field>
{
    public void Configure(EntityTypeBuilder<Field> builder)
    {
        builder.ToTable("Fields");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.NameAr)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(f => f.NameEn)
               .IsRequired()
               .HasMaxLength(200);
    }
}
