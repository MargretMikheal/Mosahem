using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;
using Mosahm.Domain.Entities.Location;
using Mosahm.Domain.Entities.Profiles;

namespace Mosahm.Persistence.Configurations.Profiles
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("Volunteers");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                    .ValueGeneratedNever();

            builder.HasIndex(v => v.NationalId).IsUnique();

            builder.Property(v => v.NationalId)
                   .IsRequired()
                   .HasMaxLength(14);

            builder.Property(v => v.Gender)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(v => v.DateOfBirth)
                   .IsRequired();

            builder.HasOne(v => v.Address)
                   .WithOne(a => a.Volunteer)
                   .HasForeignKey<Address>(a => a.VolunteerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
