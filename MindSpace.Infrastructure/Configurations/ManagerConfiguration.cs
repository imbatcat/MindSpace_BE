namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ManagerConfiguration : IEntityTypeConfiguration<SchoolManager>
{
    public void Configure(EntityTypeBuilder<SchoolManager> builder)
    {
        builder.ToTable("SchoolManager", "dbo").HasBaseType<ApplicationUser>();

        // 1 SchoolManager - 1 User 
        builder.HasOne(m => m.User)
            .WithOne(u => u.SchoolManager)
            .HasForeignKey<SchoolManager>(u => u.Id);

        // 1 School SchoolManager - 1 School
        builder
            .HasOne(m => m.School)
            .WithOne(sc => sc.SchoolManager)
            .HasForeignKey<SchoolManager>(m => m.SchoolId);
    }
}
