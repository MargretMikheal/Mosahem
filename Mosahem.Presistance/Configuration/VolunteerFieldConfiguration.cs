using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities;

namespace mosahem.Presistence.Configuration
{
    public class VolunteerFieldConfiguration
        : IEntityTypeConfiguration<VolunteerField>
    {
        public void Configure(EntityTypeBuilder<VolunteerField> builder)
        {
            builder.ToTable("VolunteerFields");

            builder.HasKey(vf => new { vf.VolunteerId, vf.FieldId });

            builder.HasOne(vf => vf.Volunteer)
                   .WithMany(v => v.VolunteerFields)
                   .HasForeignKey(vf => vf.VolunteerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vf => vf.Field)
                   .WithMany(f => f.VolunteerFields)
                   .HasForeignKey(vf => vf.FieldId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(vf => vf.VolunteerId);
            builder.HasIndex(vf => vf.FieldId);
        }
    }
}
