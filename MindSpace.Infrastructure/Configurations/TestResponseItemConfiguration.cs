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
    public class TestResponseItemConfiguration : IEntityTypeConfiguration<TestResponseItem>
    {
        public void Configure(EntityTypeBuilder<TestResponseItem> builder)
        {
            builder.ToTable("TestResponseItems");

            // 1 Test Response - M Test Response Items
            builder.HasOne(tri => tri.TestResponse)
                .WithMany(tr => tr.TestResponseItems)
                .HasForeignKey(tri => tri.TestResponseId)
                .OnDelete(DeleteBehavior.ClientCascade);

            // 1 Question Options - M Test Response Item
            // 1 Test Response Item - 0/1 Question Option
            builder.HasOne(tri => tri.SelectedOption)
                .WithMany(qo => qo.TestResponseItems)
                .HasForeignKey(tri => tri.SelectedOptionId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(tq => tq.AnswerText).IsUnicode().HasMaxLength(500);

        }
    }
}
