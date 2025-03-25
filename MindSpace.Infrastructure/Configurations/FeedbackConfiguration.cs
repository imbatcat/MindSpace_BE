namespace MindSpace.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities;

internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        // Table Name
        builder.ToTable("Feedbacks", "dbo");

        // Properties
        builder.Property(f => f.FeedbackContent)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode();

        builder.Property(f => f.Rating)
            .IsRequired()
            .HasPrecision(2, 1);

        // 1 Student - M Feedbacks
        builder.HasOne(f => f.Student)
            .WithMany(s => s.Feedbacks)
            .HasForeignKey(f => f.StudentId)
            .OnDelete(DeleteBehavior.ClientCascade);

        // 1 Psychologist - M Feedbacks
        builder.HasOne(f => f.Psychologist)
            .WithMany(p => p.Feedbacks)
            .HasForeignKey(f => f.PsychologistId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
