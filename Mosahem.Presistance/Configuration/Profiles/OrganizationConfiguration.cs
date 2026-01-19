using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Profiles;

namespace mosahem.Persistence.Configurations.Profiles
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organizations");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                   .ValueGeneratedNever();

            builder.Property(o => o.Description)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(o => o.LicenseUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(o => o.VerificationStatus)
                   .HasConversion<string>();
        }
    }
}
