using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahem.Domain.Entities;

namespace mosahem.Persistence.Configurations.Files
{
    public class TemporaryFileUploadConfiguration : IEntityTypeConfiguration<TemporaryFileUpload>
    {
        public void Configure(EntityTypeBuilder<TemporaryFileUpload> builder)
        {
            builder.ToTable("TemporaryFileUploads");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileKey)
                .IsRequired()
                .HasMaxLength(1024);

            builder.Property(x => x.FolderName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.FileKey);
        }
    }
}
