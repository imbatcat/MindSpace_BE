namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Constants;
using Domain.Entities.SupportingPrograms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Resources;

internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        // Table Name
        builder.ToTable("Resources", "dbo");

        // Properties
        builder.Property(r => r.ResourceType)
            .IsRequired()
            .HasConversion(
            convertToProviderExpression: v => v.ToString(),
            convertFromProviderExpression: v => (ResourceType)Enum.Parse(typeof(ResourceType), v.ToString()));

        builder.Property(r => r.ArticleUrl)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(r => r.Title)
            .HasMaxLength(200)
            .IsUnicode()
            .IsRequired();

        builder.Property(r => r.Introduction)
            .IsUnicode()
            .HasMaxLength(1000);

        builder.Property(r => r.ThumbnailUrl)
            .HasMaxLength(500);

        // 1 SchoolManager - M Resources
        builder.HasOne(r => r.SchoolManager)
            .WithMany(sm => sm.Resources)
            .HasForeignKey(r => r.SchoolManagerId);

        // 1 Specialization - M Resources
        builder.HasOne(r => r.Specialization)
            .WithMany(s => s.Resources)
            .HasForeignKey(r => r.SpecializationId);
    }
}
