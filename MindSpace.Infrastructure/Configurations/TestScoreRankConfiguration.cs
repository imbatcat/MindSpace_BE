using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MindSpace.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Infrastructure.Configurations
{
    public class TestScoreRankConfiguration : IEntityTypeConfiguration<TestScoreRank>
    {
        public void Configure(EntityTypeBuilder<TestScoreRank> builder)
        {
            builder.ToTable("TestScoreRanks");

            // 1 Test - M TestScoreRank
            builder.HasOne(ts => ts.Test)
                .WithMany(t => t.TestScoreRanks)
                .HasForeignKey(ts => ts.TestId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(ts => ts.Description).IsUnicode().HasMaxLength(500);
        }
    }
}
