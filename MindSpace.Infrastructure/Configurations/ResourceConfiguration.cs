using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Infrastructure.Configurations
{
    internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            // Table Name
            builder.ToTable("Resources");

            // Properties
            builder.Property(r => r.ResourceType)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (ResourceType)Enum.Parse(typeof(ResourceType), v.ToString()));

            builder.Property(r => r.ArticleUrl)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(r => r.Title)
                .HasMaxLength(200)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(r => r.Introduction)
                .IsUnicode(true)
                .HasMaxLength(1000);

            builder.Property(r => r.ThumbnailUrl)
                .HasMaxLength(500);

            // 1 Manager - M Resources
            builder.HasOne(r => r.Manager)
                .WithMany(sm => sm.Resources)
                .HasForeignKey(r => r.ManagerId);
        }
    }
}
