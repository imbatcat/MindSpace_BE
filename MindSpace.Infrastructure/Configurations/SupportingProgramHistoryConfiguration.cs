using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.SupportingPrograms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Infrastructure.Configurations
{
    public class SupportingProgramHistoryConfiguration : IEntityTypeConfiguration<SupportingProgramHistory>
    {
        public void Configure(EntityTypeBuilder<SupportingProgramHistory> builder)
        {
            // M Supporting Program - M Student
            builder.HasKey(spd => new { spd.StudentId, spd.SupportingProgramId });

            builder.HasOne(spd => spd.Student)
                .WithMany(s => s.SupportingProgramHistory)
                .HasForeignKey(spd => spd.StudentId);

            builder.HasOne(spd => spd.SupportingProgram)
                .WithMany(sp => sp.SupportingProgramHistory)
                .HasForeignKey(spd => spd.SupportingProgramId);
        }
    }
}
