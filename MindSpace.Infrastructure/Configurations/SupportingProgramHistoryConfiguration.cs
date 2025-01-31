using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Infrastructure.Configurations
{
    internal class SupportingProgramHistoryConfiguration : IEntityTypeConfiguration<SupportingProgramHistory>
    {
        public void Configure(EntityTypeBuilder<SupportingProgramHistory> builder)
        {
            builder.ToTable("SupportingProgramHistory", schema: "dbo");
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
