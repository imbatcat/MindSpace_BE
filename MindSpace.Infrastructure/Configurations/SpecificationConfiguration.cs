using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities;

namespace MindSpace.Infrastructure.Configurations
{
    internal class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
    {
        public void Configure(EntityTypeBuilder<Specification> builder)
        {
            // Properties
            builder.Property(p => p.Name).IsRequired().HasMaxLength(64).IsUnicode();
        }
    }
}
