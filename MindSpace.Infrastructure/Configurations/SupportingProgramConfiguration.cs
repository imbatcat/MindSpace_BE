using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities;


namespace MindSpace.Infrastructure.Configurations
{

    internal class SupportingProgramConfiguration : IEntityTypeConfiguration<SupportingProgram>
    {
        public void Configure(EntityTypeBuilder<SupportingProgram> builder)
        {
            // Fields
            builder.Property(sp => sp.ThumbnailUrl).IsRequired()
                .HasMaxLength(255);
            builder.Property(sp => sp.PdffileUrl).IsRequired().HasMaxLength(255);
            builder.Property(sp => sp.MaxQuantity).HasDefaultValue(0);
            builder.Property(sp => sp.StartDateAt).IsRequired();

            // 1 Supporting Program - 1 Address
            builder.OwnsOne(sp => sp.Address, address =>
            {
                address.Property(a => a.Street).IsUnicode().HasMaxLength(100);
                address.Property(a => a.City).IsUnicode().HasMaxLength(50);
                address.Property(a => a.Ward).IsUnicode().HasMaxLength(50);
                address.Property(a => a.Province).IsUnicode().HasMaxLength(50);
                address.Property(a => a.PostalCode).HasMaxLength(10);
            });

            // 1 SchoolManager - M SupportingProgram
            builder.HasOne(sp => sp.SchoolManager)
                .WithMany(m => m.SupportingPrograms)
                .HasForeignKey(sp => sp.ManagerId)
                .OnDelete(DeleteBehavior.ClientCascade);

            // 1 Psychologist - M SupportingProgram
            builder.HasOne(sp => sp.Psychologist)
                .WithMany(m => m.SupportingPrograms)
                .HasForeignKey(sp => sp.PsychologistId)
                .OnDelete(DeleteBehavior.ClientCascade);

            // 1 School - M Supporting Program
            builder.HasOne(sp => sp.School)
                .WithMany(m => m.SupportingPrograms)
                .HasForeignKey(sp => sp.SchoolId)
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
