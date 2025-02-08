namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.SupportingPrograms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class SupportingProgramHistoryConfiguration : IEntityTypeConfiguration<SupportingProgramHistory>
{
    public void Configure(EntityTypeBuilder<SupportingProgramHistory> builder)
    {
        builder.ToTable("SupportingProgramHistories", "dbo");
        // M Supporting Program - M Student
        builder.HasKey(spd => new { spd.StudentId, spd.SupportingProgramId });

        builder.HasOne(spd => spd.Student)
            .WithMany(s => s.SupportingProgramHistory)
            .HasForeignKey(spd => spd.StudentId);

        builder.HasOne(spd => spd.SupportingProgram)
            .WithMany(sp => sp.SupportingProgramHistories)
            .HasForeignKey(spd => spd.SupportingProgramId);
    }
}
