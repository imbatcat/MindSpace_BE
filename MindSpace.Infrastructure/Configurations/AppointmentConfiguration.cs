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

        // 1 PsychologistSchedule - 1 Appointment
        builder.HasOne(a => a.PsychologistSchedule)
            .WithOne(ps => ps.Appointment)
            .HasForeignKey<Appointment>(a => a.PsychologistScheduleId);

        // 1 Specialization - M Appointments
        builder.HasOne(a => a.Specialization)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.SpecializationId);

        builder.Property(a => a.MeetURL).HasMaxLength(255);

        builder.Property(a => a.Status)
            .HasConversion(
            convertToProviderExpression: s => s.ToString(),
            convertFromProviderExpression: s => Enum.Parse<AppointmentStatus>(s))
            .HasDefaultValue(AppointmentStatus.Pending);
    }
}
