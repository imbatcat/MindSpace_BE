using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    internal class TestResponseConfiguration : IEntityTypeConfiguration<TestResponse>
    {
        public void Configure(EntityTypeBuilder<TestResponse> builder)
        {
            builder.ToTable("TestResponses");

            //Properties
            builder.Property(tr => tr.TotalScore).IsRequired();

            builder.Property(tr => tr.TestScoreRankResult)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(500);

            //Relationships
            builder
                .HasOne(tr => tr.Student)
                .WithMany(st => st.TestResponses)
                .HasForeignKey(tr => tr.StudentId);

            builder
                .HasOne(tr => tr.Test)
                .WithMany(t => t.TestResponses)
                .HasForeignKey(tr => tr.TestId);
        }
    }
}
