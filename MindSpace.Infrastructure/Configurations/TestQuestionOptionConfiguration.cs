namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TestQuestionOptionConfiguration : IEntityTypeConfiguration<TestQuestionOption>
{
    public void Configure(EntityTypeBuilder<TestQuestionOption> builder)
    {
        builder.ToTable("TestQuestionOptions", "dbo");

        //Indexing
        builder.HasKey(tqo => new { tqo.TestId, tqo.TestQuestionId, tqo.QuestionOptionId }); // Composite primary key

        //Relationships
        builder
            .HasOne(tqo => tqo.Test)
            .WithMany(t => t.TestTestQuestions)
            .HasForeignKey(tqo => tqo.TestId)
            .IsRequired();

        builder
            .HasOne(tqo => tqo.TestQuestion)
            .WithMany(tq => tq.TestTestQuestions)
            .HasForeignKey(tqo => tqo.TestQuestionId)
            .IsRequired();

        builder
            .HasOne(tqo => tqo.QuestionOption)
            .WithMany(tq => tq.TestQuestionOptions)
            .HasForeignKey(tqo => tqo.QuestionOptionId)
            .IsRequired(false);
    }
}
