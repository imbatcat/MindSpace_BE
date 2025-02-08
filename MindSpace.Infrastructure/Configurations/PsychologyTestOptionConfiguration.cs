using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    internal class PsychologyTestOptionConfiguration : IEntityTypeConfiguration<PsychologyTestOption>
    {
        public void Configure(EntityTypeBuilder<PsychologyTestOption> builder)
        {
            builder.ToTable("PsychologyTestOptions", "dbo");

            // 1 Test - M PsychologyTestOptions
            builder.HasOne(pto => pto.PsychologyTest)
                .WithMany(t => t.PsychologyTestOptions)
                .HasForeignKey(qo => qo.PsychologyTestId);

            // Displayed Text
            builder.Property(qo => qo.DisplayedText).IsUnicode().HasMaxLength(500);

            // Option value must be >= 0 and <= 999
            builder.Property(qo => qo.Score)
                .IsRequired()
                .HasDefaultValue(0)
                .HasAnnotation("Range", new[] { "0", "999" });
        }
    }
}
