using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    internal class TestQuestionOptionConfiguration : IEntityTypeConfiguration<TestQuestionOption>
    {
        public void Configure(EntityTypeBuilder<TestQuestionOption> builder)
        {
            builder.ToTable("TestQuestionOptions");

            //Indexing
            builder.HasKey(tqo => new { tqo.TestId, tqo.TestQuestionId }); // Composite primary key

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
                .IsRequired();
        }
    }
}
