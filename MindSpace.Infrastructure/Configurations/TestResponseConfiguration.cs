namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            .WithMany(st => st.TestResponses)
            .HasForeignKey(tr => tr.RespondentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(tr => tr.Parent)
            .WithMany(st => st.TestResponses)
            .HasForeignKey(tr => tr.RespondentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(tr => tr.TestPublication)
            .WithMany(tp => tp.TestResponses)
            .HasForeignKey(tr => tr.TestPublicationId);
    }
}
