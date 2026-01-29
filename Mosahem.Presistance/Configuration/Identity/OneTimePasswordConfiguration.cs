using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahem.Domain.Entities.Identity;

namespace Mosahem.Persistence.Configurations.Identity
{
    public class OneTimePasswordConfiguration : IEntityTypeConfiguration<OneTimePassword>
    {
        public void Configure(EntityTypeBuilder<OneTimePassword> builder)
        {
            builder.ToTable("OneTimePasswords");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);

            builder.HasIndex(x => x.Email);
            builder.HasIndex(x => new { x.Email, x.Code });

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired(false) 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}