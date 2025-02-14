namespace MindSpace.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Resources;

internal class ResourceSectionConfiguration : IEntityTypeConfiguration<ResourceSection>
{
    public void Configure(EntityTypeBuilder<ResourceSection> builder)
    {
        // Table Name
        builder.ToTable("ResourceSections", "dbo");

        // Properties
        builder.Property(rs => rs.Heading)
            .HasMaxLength(255)
            .IsUnicode()
            .IsRequired();

        builder.Property(rs => rs.HtmlContent)
            .HasMaxLength(255)
            .IsRequired()
            .IsUnicode();

        // 1 Resource - M ResourceSection
        builder.HasOne(rs => rs.Resource)
            .WithMany(r => r.ResourceSections)
            .HasForeignKey(rs => rs.ResourceId);
    }
}
