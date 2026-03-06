using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        builder.Property(s => s.FieldId)
              .IsRequired();

        builder.HasOne(s => s.Field)
               .WithMany()
               .HasForeignKey(s => s.FieldId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => s.FieldId);
    }
}
