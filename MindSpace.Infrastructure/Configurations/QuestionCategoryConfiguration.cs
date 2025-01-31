using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Infrastructure.Configurations
{
    internal class QuestionCategoryConfiguration : IEntityTypeConfiguration<QuestionCategory>
    {
        public void Configure(EntityTypeBuilder<QuestionCategory> builder)
        {
            builder.ToTable("QuestionCategories", schema: "dbo");
            // Properties
            builder.Property(qc => qc.Title).IsRequired().HasMaxLength(100).IsUnicode();
        }
    }
}
