using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;

namespace Mosahm.Presistence.Configuration
{
    public class VolunteerSkillConfiguration
        : IEntityTypeConfiguration<VolunteerSkill>
    {
        public void Configure(EntityTypeBuilder<VolunteerSkill> builder)
        {
            builder.ToTable("VolunteerSkills");

            builder.HasKey(vs => new { vs.VolunteerId, vs.SkillId });

            builder.HasOne(vs => vs.Volunteer)
                   .WithMany(v => v.VolunteerSkills)
                   .HasForeignKey(vs => vs.VolunteerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vs => vs.Skill)
                   .WithMany(s => s.VolunteerSkills)
                   .HasForeignKey(vs => vs.SkillId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(vs => vs.VolunteerId);
            builder.HasIndex(vs => vs.SkillId);
        }
    }
}
