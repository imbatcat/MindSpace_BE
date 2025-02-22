namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Identity;

internal class TestResponseConfiguration : IEntityTypeConfiguration<TestResponse>
{
    public void Configure(EntityTypeBuilder<TestResponse> builder)
    {
        builder.ToTable("TestResponses", "dbo");

        //Properties
        builder.Property(tr => tr.TotalScore).IsRequired(false);

        builder.Property(tr => tr.TestScoreRankResult)
            .IsUnicode()
            .IsRequired(false)
            .HasMaxLength(500);

        //Relationships
        builder
            .HasOne(tr => tr.Student)
            .WithMany(s => s.TestResponses)
            .HasForeignKey(tr => tr.StudentId)
            .IsRequired(false);

        builder
            .HasOne(tr => tr.Parent)
            .WithMany(s => s.TestResponses)
            .HasForeignKey(tr => tr.ParentId)
            .IsRequired(false);

        builder
            .HasOne(tr => tr.Test)
            .WithMany(t => t.TestResponses)
            .HasForeignKey(tr => tr.TestId);
    }
}
