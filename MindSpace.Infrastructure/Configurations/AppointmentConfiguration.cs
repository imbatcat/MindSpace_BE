namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Appointments;
using Domain.Entities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments", "dbo");

        // 1 Student - M Appointments
        builder.HasOne(a => a.Student)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.ClientCascade);

        // 1 Psychologist - M Appointments
        builder.HasOne(a => a.Psychologist)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PsychologistId)
            .OnDelete(DeleteBehavior.ClientCascade);

        // 1 Specialization - M Appointments
        builder.HasOne(a => a.Specialization)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.SpecializationId);

        // 1 PsychologistSchedule - M Appointments
        builder.HasOne(a => a.PsychologistSchedule)
            .WithMany(ps => ps.Appointments)
            .HasForeignKey(a => a.PsychologistScheduleId);

        builder.Property(a => a.MeetURL).HasMaxLength(255);

        builder.Property(a => a.Status)
            .HasConversion(
            convertToProviderExpression: s => s.ToString(),
            convertFromProviderExpression: s => Enum.Parse<AppointmentStatus>(s))
            .HasDefaultValue(AppointmentStatus.Pending);
    }
}
