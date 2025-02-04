namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Constants;
using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
{
    public void Configure(EntityTypeBuilder<TestQuestion> builder)
    {
        builder.ToTable("TestQuestions", "dbo");

        // 1 QuestionCategory - M TestQuestions
        builder
            .HasOne(tq => tq.QuestionCategory)
            .WithMany()
            .HasForeignKey(qo => qo.QuestionCategoryId)
            .IsRequired(false);

        builder.Property(a => a.QuestionFormat)
            .IsRequired()
            .HasConversion(
            convertToProviderExpression: s => s.ToString(),
            convertFromProviderExpression: s => Enum.Parse<QuestionFormats>(s))
            .HasDefaultValue(QuestionFormats.MultipleChoice);
    }
}
