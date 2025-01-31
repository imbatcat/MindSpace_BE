using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    public class TestResponseItemConfiguration : IEntityTypeConfiguration<TestResponseItem>
    {
        public void Configure(EntityTypeBuilder<TestResponseItem> builder)
        {
            builder.ToTable("TestResponseItems", schema: "dbo");

            // 1 Test Response - M Test Response Items
            builder.HasOne(tri => tri.TestResponse)
                .WithMany(tr => tr.TestResponseItems)
                .HasForeignKey(tri => tri.TestResponseId);

            // Question Title
            builder.Property(tri => tri.QuestionTitle).IsUnicode().HasMaxLength(500);

            // Answer Text
            builder.Property(tri => tri.AnswerText).IsUnicode().HasMaxLength(500);

            // Score must be >= 0 and <= 999
            builder.Property(tri => tri.Score)
                .IsRequired()
                .HasDefaultValue(0)
                .HasAnnotation("Range", new[] { "0", "999" });

            builder.Property(tri => tri.IsTextArea)
                .HasColumnType("bit");
        }
    }
}
