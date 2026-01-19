using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Persistence.Configurations.Opportunities
{
    public class OpportunityFieldConfiguration : IEntityTypeConfiguration<OpportunityField>
    {
        public void Configure(EntityTypeBuilder<OpportunityField> builder)
        {
            builder.ToTable("OpportunityFields");

            builder.HasKey(of => new { of.OpportunityId, of.FieldId });

            builder.HasOne(of => of.Opportunity)
                   .WithMany(o => o.OpportunityFields)
                   .HasForeignKey(of => of.OpportunityId);

            builder.HasOne(of => of.Field)
                   .WithMany(f => f.OpportunityFields)
                   .HasForeignKey(of => of.FieldId);
        }
    }
}
