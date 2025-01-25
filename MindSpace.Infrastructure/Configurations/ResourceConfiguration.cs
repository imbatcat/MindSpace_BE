using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.SupportingPrograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Infrastructure.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
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

            // 1 SchoolManager - M Resources
            builder.HasOne(r => r.SchoolManager)
                .WithMany(sm => sm.Resources)
                .HasForeignKey(r => r.SchoolManagerId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
