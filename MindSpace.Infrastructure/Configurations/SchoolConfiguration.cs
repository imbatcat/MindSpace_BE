namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class SchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.ToTable("Schools", "dbo");

        // Indexing
        builder.HasIndex(s => s.ContactEmail).IsUnique();
        builder.HasIndex(s => s.PhoneNumber).IsUnique();

        // Properties
        builder.Property(s => s.SchoolName).IsUnicode().IsRequired().HasMaxLength(100);
        builder.Property(s => s.ContactEmail).IsRequired().HasMaxLength(100);
        builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(11);
        builder.Property(s => s.Description).IsUnicode().HasMaxLength(100);
        builder.Property(s => s.JoinDate).IsRequired().HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();

        builder.Property(s => s.ManagerId).IsRequired(false);

        // 1 School - Owns 1 Address
        builder.OwnsOne(navigationExpression: s => s.Address,
        buildAction: address =>
        {
            address.Property(a => a.Street).IsUnicode().HasMaxLength(100);
            address.Property(a => a.City).IsUnicode().HasMaxLength(50);
            address.Property(a => a.Ward).IsUnicode().HasMaxLength(50);
            address.Property(a => a.Province).IsUnicode().HasMaxLength(50);
            address.Property(a => a.PostalCode).HasMaxLength(10);
        });
    }
}
