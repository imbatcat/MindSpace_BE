namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.ToTable("QuestionOptions", "dbo");

        // 1 Question - M QuestionOptions
        builder.HasOne(qo => qo.Question)
            .WithMany(q => q.QuestionOptions)
            .HasForeignKey(qo => qo.QuestionId)
            .IsRequired(false);

        // Displayed Text
        builder.Property(qo => qo.DisplayedText).IsUnicode().HasMaxLength(500);
    }
}
