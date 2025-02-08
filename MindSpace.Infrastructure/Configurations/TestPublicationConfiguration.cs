using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    internal class TestPublicationConfiguration : IEntityTypeConfiguration<TestPublication>
    {
        public void Configure(EntityTypeBuilder<TestPublication> builder)
        {
            builder.ToTable("TestPublications", "dbo");

            //Properties
            builder.Property(t => t.Title).IsUnicode().IsRequired(false).HasMaxLength(100);
            builder.Property(t => t.StartDate).IsRequired(false);
            builder.Property(t => t.EndDate).IsRequired(false);

            builder
                .HasOne(tp => tp.Test)
                .WithMany(tc => tc.TestPublications)
                .HasForeignKey(t => t.TestId);

            builder.Property(t => t.TestPublicationStatus)
            .HasConversion(
            convertToProviderExpression: v => v.ToString(),
            convertFromProviderExpression: v => (TestPublicationStatus)Enum.Parse(typeof(TestPublicationStatus), v)) // convert status to string for db
            .HasDefaultValue(TestPublicationStatus.Enabled); // set defaults to 'Enabled'

            builder
                .HasOne(tp => tp.SchoolManager)
                .WithMany(m => m.TestPublications)
                .HasForeignKey(t => t.ManagerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
