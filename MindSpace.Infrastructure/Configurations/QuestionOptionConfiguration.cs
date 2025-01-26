using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MindSpace.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Infrastructure.Configurations
{
    public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
    {
        public void Configure(EntityTypeBuilder<QuestionOption> builder)
        {
            builder.ToTable("QuestionOptions");

            // 1 TestQuestion - M QuestionOptions
            builder.HasOne(qo => qo.TestQuestion)
                .WithMany(tq => tq.QuestionOptions)
                .HasForeignKey(qo => qo.TestQuestionId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(qo => qo.OptionText).IsUnicode().HasMaxLength(500);
            builder.Property(qo => qo.FixedType)
                .HasConversion(
                    s => s.ToString(),
                    s => Enum.Parse<FixedType>(s));
        }
    }
}
