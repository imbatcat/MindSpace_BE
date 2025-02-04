namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.ToTable("Manager", "dbo").HasBaseType<ApplicationUser>();

        // 1 Manager - 1 User 
        builder.HasOne(m => m.User)
            .WithOne(u => u.Manager)
            .HasForeignKey<Manager>(u => u.Id);

        // 1 School Manager - 1 School
        builder
            .HasOne(m => m.School)
            .WithOne(sc => sc.Manager)
            .HasForeignKey<Manager>(m => m.SchoolId);
    }
}
