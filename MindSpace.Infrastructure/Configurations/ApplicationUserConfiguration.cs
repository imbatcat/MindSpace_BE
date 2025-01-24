using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //Indexing
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            //Properties
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(u => u.UpdatedAt).HasDefaultValueSql("getdate()");
            builder.Property(u => u.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserStatus)Enum.Parse(typeof(UserStatus), v))
                .HasDefaultValue(UserStatus.Enabled);
            builder.Property(u => u.ImageUrl).HasMaxLength(-1);
            builder.Property(u => u.FullName).HasMaxLength(256).IsUnicode();

            //Relationships
            builder
                .HasOne(au => au.Student_School)
                .WithMany(s => s.Students)
                .HasForeignKey(au => au.Student_SchoolId);

            builder
                .HasOne(au => au.Manager_School)
                .WithOne(s => s.SchoolManager)
                .HasForeignKey<ApplicationUser>(au => au.Manager_SchoolId);
        }
    }
}