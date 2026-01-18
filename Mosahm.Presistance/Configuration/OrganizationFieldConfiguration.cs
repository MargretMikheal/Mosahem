using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;

namespace Mosahm.Persistence.Configurations
{
    public class OrganizationFieldConfiguration
        : IEntityTypeConfiguration<OrganizationField>
    {
        public void Configure(EntityTypeBuilder<OrganizationField> builder)
        {
            builder.ToTable("OrganizationFields");

            builder.HasKey(of => new { of.OrganizationId, of.FieldId });

            builder.HasOne(of => of.Organization)
                .WithMany(o => o.OrganizationFields)
                .HasForeignKey(of => of.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(of => of.Field)
                .WithMany(f => f.OrganizationFields)
                .HasForeignKey(of => of.FieldId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
