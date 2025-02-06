namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TestScoreRankConfiguration : IEntityTypeConfiguration<TestScoreRank>
{
    public void Configure(EntityTypeBuilder<TestScoreRank> builder)
    {
        builder.ToTable("TestScoreRanks", "dbo", buildAction: t => { t.HasCheckConstraint("CK_TestScore_ScoreOrder", "[MaxScore] >= [MinScore]"); });

        // 1 Test - M TestScoreRank
        builder.HasOne(ts => ts.Test)
            .WithMany(t => t.TestScoreRanks)
            .HasForeignKey(ts => ts.TestId);

        // Scores must be >= 0 and <= 999
        builder.Property(ts => ts.MinScore)
            .IsRequired()
            .HasAnnotation("Range", new[] { "0", "999" });

        builder.Property(ts => ts.MaxScore)
            .IsRequired()
            .HasAnnotation("Range", new[] { "0", "999" });

        builder.Property(ts => ts.Result).IsUnicode().HasMaxLength(500);
    }
}
