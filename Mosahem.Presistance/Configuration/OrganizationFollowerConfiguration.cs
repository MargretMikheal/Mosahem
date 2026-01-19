using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities;

namespace mosahem.Persistence.Configurations
{
    public class OrganizationFollowerConfiguration
        : IEntityTypeConfiguration<OrganizationFollower>
    {
        public void Configure(EntityTypeBuilder<OrganizationFollower> builder)
        {
            builder.ToTable("OrganizationFollowers");

            builder.HasKey(of => new { of.VolunteerId, of.OrganizationId });

            builder.HasOne(of => of.Volunteer)
                .WithMany(v => v.OrganizationFollwers)
                .HasForeignKey(of => of.VolunteerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(of => of.Organization)
                .WithMany(o => o.OrganizationFollwers)
                .HasForeignKey(of => of.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(of => of.OrganizationId);
        }
    }
}
