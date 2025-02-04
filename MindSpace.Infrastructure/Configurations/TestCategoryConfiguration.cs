namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TestCategoryConfiguration : IEntityTypeConfiguration<TestCategory>
{
    public void Configure(EntityTypeBuilder<TestCategory> builder)
    {
        builder.ToTable("TestCategories", "dbo");
        //Properties
        builder.Property(tc => tc.Name).IsRequired().HasMaxLength(100);
    }
}
