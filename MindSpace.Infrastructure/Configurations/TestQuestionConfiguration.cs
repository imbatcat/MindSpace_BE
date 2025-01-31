using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    internal class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.ToTable("TestQuestions", schema: "dbo");

            // 1 QuestionCategory - M TestQuestions
            builder
                .HasOne(tq => tq.QuestionCategory)
                .WithMany()
                .HasForeignKey(qo => qo.QuestionCategoryId)
                .IsRequired(false);

            builder.Property(a => a.QuestionFormat)
                .IsRequired()
                .HasConversion(
                    s => s.ToString(),
                    s => Enum.Parse<QuestionFormats>(s))
                .HasDefaultValue(QuestionFormats.MultipleChoice);
        }
    }
}