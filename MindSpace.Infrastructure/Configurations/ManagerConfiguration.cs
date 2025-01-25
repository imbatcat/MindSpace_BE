using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // 1 SchoolManager - 1 School
            builder
                .HasOne(m => m.School)
                .WithOne(sc => sc.SchoolManager)
                .HasForeignKey<Manager>(m => m.SchoolId);
        }
    }
}
