namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Appointments;
using Domain.Entities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class PsychologistScheduleConfiguration : IEntityTypeConfiguration<PsychologistSchedule>
{
    public void Configure(EntityTypeBuilder<PsychologistSchedule> builder)
    {
        builder.ToTable("PsychologistSchedules", "dbo");

        // 1 Psychologist - M PsychologistSchedules
        builder.HasOne(ps => ps.Psychologist)
            .WithMany(p => p.PsychologistSchedules)
            .HasForeignKey(ps => ps.PsychologistId);

        builder.Property(ps => ps.StartTime)
            .HasColumnType("time");

        builder.Property(ps => ps.EndTime)
            .HasColumnType("time");

        builder.Property(ps => ps.Date)
            .HasColumnType("date");

        builder.Property(ps => ps.Status)
            .HasConversion(
            convertToProviderExpression: s => s.ToString(),
            convertFromProviderExpression: s => Enum.Parse<PsychologistScheduleStatus>(s))
            .HasDefaultValue(PsychologistScheduleStatus.Free);
    }
}
