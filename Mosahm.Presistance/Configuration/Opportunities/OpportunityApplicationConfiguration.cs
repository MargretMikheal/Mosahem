using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;
using Mosahm.Domain.Entities.Opportunities;

namespace Mosahm.Persistence.Configurations.Opportunities
{
    public class OpportunityApplicationConfiguration
        : IEntityTypeConfiguration<OpportunityApplication>
    {
        public void Configure(EntityTypeBuilder<OpportunityApplication> builder)
        {
            builder.ToTable("OpportunityApplications");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ApplicantStatus)
                   .HasConversion<string>();

            builder.HasOne(a => a.Volunteer)
                   .WithMany(v => v.OpportunityApplications)
                   .HasForeignKey(a => a.VolunteerId);

            builder.HasOne(a => a.Opportunity)
                   .WithMany(o => o.OpportunityApplications)
                   .HasForeignKey(a => a.OpportunityId);
        }
    }
}
