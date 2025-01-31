using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Infrastructure.Configurations
{
    internal class PsychologistConfiguration : IEntityTypeConfiguration<Psychologist>
    {
        public void Configure(EntityTypeBuilder<Psychologist> builder)
        {
            // Create TPT for Psychologist
            builder.ToTable("Psychologists", schema: "dbo").HasBaseType<ApplicationUser>();

            // Fields
            builder.Property(p => p.Bio)
                .HasMaxLength(1000).IsUnicode(true);

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
}
