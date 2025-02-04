namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Constants;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Indexing
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

        // Properties
        builder.Property(u => u.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
        builder.Property(u => u.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
        builder.Property(u => u.Status)
            .HasConversion(
            convertToProviderExpression: v => v.ToString(),
            convertFromProviderExpression: v => (UserStatus)Enum.Parse(typeof(UserStatus), v))
            .HasDefaultValue(UserStatus.Enabled);
        builder.Property(u => u.ImageUrl).HasMaxLength(-1);
        builder.Property(u => u.FullName).HasMaxLength(256).IsUnicode();
    }
}
