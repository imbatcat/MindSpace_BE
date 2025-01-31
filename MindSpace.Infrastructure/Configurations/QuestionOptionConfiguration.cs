using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
    {
        public void Configure(EntityTypeBuilder<QuestionOption> builder)
        {
            builder.ToTable("QuestionOptions");

            // 1 TestQuestion - M QuestionOptions
            builder.HasOne(qo => qo.TestQuestion)
                .WithMany(tq => tq.QuestionOptions)
                .HasForeignKey(qo => qo.TestQuestionId)
                .IsRequired(false);

            // Option Text
            builder.Property(qo => qo.OptionText).IsUnicode().HasMaxLength(500);

            // Score must be >= 0 and <= 999
            builder.Property(qo => qo.Score)
                .IsRequired()
                .HasDefaultValue(0)
                .HasAnnotation("Range", new[] { "0", "999" });

            //builder.Property(qo => qo.IsTextArea)
            //    .HasColumnType("bit");

            // 1 QuestionCategory - M QuestionOptions
            builder
                .HasOne(qo => qo.QuestionCategory)
                .WithMany(qc => qc.QuestionOptions)
                .HasForeignKey(qo => qo.QuestionCategoryId)
                .IsRequired(false);
        }
    }
}
