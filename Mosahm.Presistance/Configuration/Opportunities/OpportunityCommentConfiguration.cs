using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;
using Mosahm.Domain.Entities.Opportunities;

public class OpportunityCommentConfiguration
    : IEntityTypeConfiguration<OpportunityComment>
{
    public void Configure(EntityTypeBuilder<OpportunityComment> builder)
    {
        builder.ToTable("OpportunityComments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Text)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(c => c.IsHidden)
               .HasDefaultValue(false);

        builder.HasOne(c => c.Volunteer)
               .WithMany(v => v.OpportunityComments)
               .HasForeignKey(c => c.VolunteerId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Opportunity)
               .WithMany(o => o.OpportunityComments)
               .HasForeignKey(c => c.OpportunityId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.OpportunityId);
    }
}
