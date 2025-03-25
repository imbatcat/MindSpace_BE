namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ParentConfiguration : IEntityTypeConfiguration<Parent>
{
    public void Configure(EntityTypeBuilder<Parent> builder)
    {
        // TPT mapping (Table Per Type)
        builder.ToTable("Parents", "dbo").HasBaseType<ApplicationUser>();

        // 1 Parent - 1 User
        builder.HasOne(p => p.User)
            .WithOne(p => p.Parent)
            .HasForeignKey<Parent>(p => p.Id);
    }
}