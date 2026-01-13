using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mosahm.Domain.Entities;
using Mosahm.Domain.Entities.Questions;
using System.Text.Json;

public class QuestionAnswerConfiguration : IEntityTypeConfiguration<QuestionAnswer>
{
    public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
    {
        builder.ToTable("QuestionAnswers");

        builder.HasKey(qa => qa.Id);

        builder.Property(qa => qa.AnswerText)
               .HasMaxLength(2000);

        builder.Property(qa => qa.Json)
               .HasConversion(
                   v => v == null ? null : v.RootElement.GetRawText(),
                   v => v == null ? null : JsonDocument.Parse(v))
               .HasColumnType("nvarchar(max)");

        builder.HasOne(qa => qa.Question)
               .WithMany(q => q.QuestionAnswers)
               .HasForeignKey(qa => qa.QuestionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(qa => qa.Volunteer)
               .WithMany(v => v.QuestionAnswers)
               .HasForeignKey(qa => qa.VolunteerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
