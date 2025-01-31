using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    internal class QuestionCategoryConfiguration : IEntityTypeConfiguration<QuestionCategory>
    {
        public void Configure(EntityTypeBuilder<QuestionCategory> builder)
        {
            builder.ToTable("QuestionCategories");
            // Properties
            builder.Property(qc => qc.Title).IsRequired().HasMaxLength(100).IsUnicode();
        }
    }
}
