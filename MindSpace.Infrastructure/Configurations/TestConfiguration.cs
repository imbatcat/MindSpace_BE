namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Constants;
using Domain.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("Tests", "dbo");

        //Indexing
        builder.HasIndex(t => new { t.TestCode, t.AuthorId }).IsUnique(); // Unique trên TestCode và AuthorId (ManagerId)
        builder.HasIndex(t => new { t.Title, t.AuthorId }).IsUnique(); // Unique trên Title và AuthorId

        //Properties
        builder.Property(t => t.Title).IsUnicode().IsRequired().HasMaxLength(150);
        builder.Property(t => t.TestCode).IsRequired().HasMaxLength(20);
        builder.Property(t => t.TargetUser)
            .HasConversion(
            convertToProviderExpression: v => v.ToString(),
            convertFromProviderExpression: v => (TargetUser)Enum.Parse(typeof(TargetUser), v))
            .HasDefaultValue(TargetUser.Everyone);
        builder.Property(t => t.Description).IsUnicode().HasMaxLength(500);
        builder.Property(t => t.QuestionCount).HasDefaultValue(0);
        builder.Property(t => t.Price)
            .HasPrecision(10, 2)
            .IsRequired()
            .HasAnnotation("Range", new[] { "0", "999999.99" });
        builder.Property(t => t.TestStatus)
            .HasConversion(
            convertToProviderExpression: v => v.ToString(),
            convertFromProviderExpression: v => (TestStatus)Enum.Parse(typeof(TestStatus), v)) // convert status to string for db
            .HasDefaultValue(TestStatus.Enabled); // set defaults to 'Enabled'
        //Relationships
        builder
            .HasOne(t => t.TestCategory)
            .WithMany(tc => tc.Tests)
            .HasForeignKey(t => t.TestCategoryId);
        builder
            .HasOne(t => t.Specialization)
            .WithMany(s => s.Tests)
            .HasForeignKey(t => t.SpecializationId);
        builder
            .HasOne(t => t.Author)
            .WithMany()
            .HasForeignKey(t => t.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
