using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Opportunities;

public class OpportunitySaveConfiguration
    : IEntityTypeConfiguration<OpportunitySave>
{
    public void Configure(EntityTypeBuilder<OpportunitySave> builder)
    {
        builder.ToTable("OpportunitySaves");

        builder.HasKey(s => new { s.VolunteerId, s.OpportunityId });

        builder.HasOne(s => s.Volunteer)
               .WithMany(v => v.OpportunitySaves)
               .HasForeignKey(s => s.VolunteerId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Opportunity)
               .WithMany(o => o.OpportunitySaves)
               .HasForeignKey(s => s.OpportunityId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
