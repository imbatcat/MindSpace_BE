using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MindSpace.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
