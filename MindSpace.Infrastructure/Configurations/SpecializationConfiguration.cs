namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.ToTable("Specializations", "dbo");
        // Properties
        builder.Property(p => p.Name).IsRequired().HasMaxLength(64).IsUnicode();
    }
}
