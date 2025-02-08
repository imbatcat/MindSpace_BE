namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.SupportingPrograms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class SupportingProgramConfiguration : IEntityTypeConfiguration<SupportingProgram>
{
    public void Configure(EntityTypeBuilder<SupportingProgram> builder)
    {
        builder.ToTable("SupportingPrograms", "dbo");

        // Fields
        builder.Property(sp => sp.ThumbnailUrl).IsRequired()
            .HasMaxLength(255);
        builder.Property(sp => sp.PdffileUrl).IsRequired().HasMaxLength(255);
        builder.Property(sp => sp.MaxQuantity).HasDefaultValue(0);
        builder.Property(sp => sp.StartDateAt).IsRequired();

        // 1 Supporting Program - 1 Address
        builder.OwnsOne(navigationExpression: sp => sp.Address,
        buildAction: address =>
        {
            address.Property(a => a.Street).IsUnicode().HasMaxLength(100);
            address.Property(a => a.City).IsUnicode().HasMaxLength(50);
            address.Property(a => a.Ward).IsUnicode().HasMaxLength(50);
            address.Property(a => a.Province).IsUnicode().HasMaxLength(50);
            address.Property(a => a.PostalCode).HasMaxLength(10);
        });

        // 1 SchoolManager - M SupportingProgramSpecification
        builder.HasOne(sp => sp.SchoolManager)
            .WithMany(m => m.SupportingPrograms)
            .HasForeignKey(sp => sp.SchoolManagerId)
            .OnDelete(DeleteBehavior.ClientCascade);

        // 1 Psychologist - M SupportingProgramSpecification
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
