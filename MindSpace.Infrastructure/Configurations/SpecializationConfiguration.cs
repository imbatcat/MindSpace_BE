using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities;

namespace MindSpace.Infrastructure.Configurations
{
    internal class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            // Properties
            builder.Property(p => p.Name).IsRequired().HasMaxLength(64).IsUnicode();
        }
    }
}
