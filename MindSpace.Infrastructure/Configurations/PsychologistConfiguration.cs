namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class PsychologistConfiguration : IEntityTypeConfiguration<Psychologist>
{
    public void Configure(EntityTypeBuilder<Psychologist> builder)
    {
        // Create TPT for Psychologist
        builder.ToTable("Psychologists", "dbo").HasBaseType<ApplicationUser>();

        // Fields
        builder.Property(p => p.Bio)
            .IsRequired(false)
            .HasMaxLength(1000).IsUnicode();

        builder.Property(p => p.AverageRating)
            .HasColumnType("float");

        builder.Property(p => p.SessionPrice)
            .HasColumnType("decimal(18, 2)");

        builder.Property(p => p.ComissionRate)
            .HasColumnType("decimal(5, 2)");

        // 1 Specialization - 1 User
        builder.HasOne(p => p.User)
            .WithOne(u => u.Psychologist)
            .HasForeignKey<Psychologist>(p => p.Id);

        // 1 Specialization - M Psychologists
        builder.HasOne(p => p.Specialization)
            .WithMany(s => s.Psychologists)
            .HasForeignKey(p => p.SpecializationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
