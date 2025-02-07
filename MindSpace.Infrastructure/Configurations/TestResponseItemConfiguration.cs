namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TestResponseItemConfiguration : IEntityTypeConfiguration<TestResponseItem>
{
    public void Configure(EntityTypeBuilder<TestResponseItem> builder)
    {
        builder.ToTable("TestResponseItems", "dbo");

        // 1 Test Response - M Test Response Items
        builder.HasOne(tri => tri.TestResponse)
            .WithMany(tr => tr.TestResponseItems)
            .HasForeignKey(tri => tri.TestResponseId);

        // Question Content
        builder.Property(tri => tri.QuestionContent).IsUnicode().HasMaxLength(500);

        // Answer Text
        builder.Property(tri => tri.AnswerText).IsUnicode().HasMaxLength(500);

        // Score must be >= 0 and <= 999
        builder.Property(tri => tri.Score)
            .IsRequired(false)
            .HasDefaultValue(0)
            .HasAnnotation("Range", new[] { "0", "999" });
    }
}
