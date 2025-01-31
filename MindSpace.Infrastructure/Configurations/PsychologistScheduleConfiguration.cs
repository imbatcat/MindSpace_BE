using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Infrastructure.Configurations
{
    public class PsychologistScheduleConfiguration : IEntityTypeConfiguration<PsychologistSchedule>
    {

        public void Configure(EntityTypeBuilder<PsychologistSchedule> builder)
        {
            builder.ToTable("PsychologistSchedules", schema: "dbo");

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
                    s => s.ToString(),
                    s => Enum.Parse<PsychologistScheduleStatus>(s))
                .HasDefaultValue(PsychologistScheduleStatus.Free);
        }
    }
}
