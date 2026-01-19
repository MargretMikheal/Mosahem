using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Profiles;

namespace mosahem.Persistence.Configurations.Profiles
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
