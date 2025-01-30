using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    internal class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.ToTable("TestQuestions");

            // 1 QuestionCategory - M TestQuestions
            builder
                .HasOne(tq => tq.QuestionCategory)
                .WithMany()
                .HasForeignKey(qo => qo.QuestionCategoryId)
                .IsRequired(false);
        }
    }
}