using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.MasterData;

public class SkillConfiguration
    : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.NameAr)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(s => s.NameEn)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(s => s.Category)
               .HasMaxLength(100);
    }
}
