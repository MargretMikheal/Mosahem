using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities.Identity;
using Mosahm.Domain.Entities.Profiles;

namespace Mosahm.Persistence.Configurations.Identity
{
    public class MosahmUserConfiguration : IEntityTypeConfiguration<MosahmUser>
    {
        public void Configure(EntityTypeBuilder<MosahmUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            builder.HasOne(u => u.Volunteer)
                   .WithOne(v => v.User)
                   .HasForeignKey<Volunteer>(v => v.Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Organization)
                   .WithOne(o => o.User)
                   .HasForeignKey<Organization>(o => o.Id)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
