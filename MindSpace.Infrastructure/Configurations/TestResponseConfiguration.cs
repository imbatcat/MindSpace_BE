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
            .HasOne(s => s.Student)
            .WithMany()
            .HasForeignKey(tr => tr.StudentId)
            .HasPrincipalKey(s => s.Id)
            .IsRequired(false);

        builder
            .HasOne(p => p.Parent)
            .WithMany()
            .HasForeignKey(tr => tr.ParentId)
            .HasPrincipalKey(p => p.Id)
            .IsRequired(false);

        builder
            .HasOne(tr => tr.TestPublication)
            .WithMany(tp => tp.TestResponses)
            .HasForeignKey(tr => tr.TestPublicationId);
    }
}
