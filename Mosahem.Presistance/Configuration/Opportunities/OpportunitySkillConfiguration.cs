using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;

namespace mosahem.Persistence.Configurations.Opportunities
{
    public class OpportunitySkillConfiguration : IEntityTypeConfiguration<OpportunitySkill>
    {
        public void Configure(EntityTypeBuilder<OpportunitySkill> builder)
        {
            builder.ToTable("OpportunitySkills");

            builder.HasKey(os => os.Id);

            builder.HasDiscriminator(os => os.SkillType)
                   .HasValue<OpportunityRequireSkill>(OpportunitySkillType.Require)
                   .HasValue<OpportunityProvideSkill>(OpportunitySkillType.Provide);
            
            builder.HasIndex(os => new { os.OpportunityId, os.SkillId, os.SkillType })
                    .IsUnique();

            builder.HasOne(os => os.Opportunity)
                   .WithMany(o => o.OpportunitySkills)
                   .HasForeignKey(os => os.OpportunityId);

            builder.HasOne(os => os.Skill)
                   .WithMany(s => s.OpportunitySkills)
                   .HasForeignKey(os => os.SkillId);
        }
    }
}
