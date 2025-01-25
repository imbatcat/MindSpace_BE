using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Infrastructure.Configurations
{
    internal class ManagerConfiguration : IEntityTypeConfiguration<SchoolManager>
    {
        public void Configure(EntityTypeBuilder<SchoolManager> builder)
        {
            builder.ToTable("SchoolManager").HasBaseType<ApplicationUser>();

            // 1 SchoolManager - 1 User 
            builder.HasOne(m => m.User)
                .WithOne(u => u.Manager)
                .HasForeignKey<SchoolManager>(u => u.Id);

            // 1 School Manager - 1 School
            builder
               .HasOne(m => m.School)
               .WithOne(sc => sc.SchoolManager)
               .HasForeignKey<SchoolManager>(m => m.SchoolId);
        }
    }
}
