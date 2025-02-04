namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        //TPT mapping
        builder.ToTable("Students", "dbo").HasBaseType<ApplicationUser>();

        //Properties
        builder
            .HasOne(s => s.User)
            .WithOne(au => au.Student)
            .HasForeignKey<Student>(s => s.Id);
        builder
            .HasOne(st => st.School)
            .WithMany(sc => sc.Students)
            .HasForeignKey(st => st.SchoolId);
    }
}
