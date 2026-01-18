using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;
using Mosahm.Domain.Entities.Opportunities;

public class OpportunityLikeConfiguration
    : IEntityTypeConfiguration<OpportunityLike>
{
    public void Configure(EntityTypeBuilder<OpportunityLike> builder)
    {
        builder.ToTable("OpportunityLikes");

        builder.HasKey(l => new { l.VolunteerId, l.OpportunityId });

        builder.HasOne(l => l.Volunteer)
               .WithMany(v => v.OpportunityLikes)
               .HasForeignKey(l => l.VolunteerId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l => l.Opportunity)
               .WithMany(o => o.OpportunityLikes)
               .HasForeignKey(l => l.OpportunityId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
