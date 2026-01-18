using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;
using Mosahm.Domain.Entities.Opportunities;

namespace Mosahm.Persistence.Configurations.Opportunities
{
    public class OpportunityConfiguration : IEntityTypeConfiguration<Opportunity>
    {
        public void Configure(EntityTypeBuilder<Opportunity> builder)
        {
            builder.ToTable("Opportunities");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Title)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.HasIndex(o => o.OrganizationId);

            builder.Property(o => o.Descripition)
                   .IsRequired()
                   .HasMaxLength(5000);

            builder.Property(o => o.Status)
                   .HasConversion<string>();

            builder.Property(o => o.WorkType)
                   .HasConversion<string>();

            builder.Property(o => o.LocationType)
                   .HasConversion<string>();

            builder.HasOne(o => o.Organization)
                   .WithMany(org => org.Opportunities)
                   .HasForeignKey(o => o.OrganizationId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
