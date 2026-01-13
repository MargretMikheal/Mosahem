using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;
using Mosahm.Domain.Entities.Questions;
using System.Text.Json;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(q => q.Id);
        builder.HasIndex(q => new { q.OpportunityId, q.Order })
       .IsUnique();

        builder.Property(q => q.Order)
               .IsRequired();

        builder.Property(q => q.Description)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(q => q.AnswerType)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(q => q.IsRequired)
               .IsRequired();

        builder.Property(q => q.Options)
                   .HasConversion(
                       v => v == null ? null : v.RootElement.GetRawText(),
                       v => v == null ? null : JsonDocument.Parse(v))
                   .HasColumnType("nvarchar(max)");

        builder.HasOne(q => q.Opportunity)
               .WithMany(o => o.Questions)
               .HasForeignKey(q => q.OpportunityId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
