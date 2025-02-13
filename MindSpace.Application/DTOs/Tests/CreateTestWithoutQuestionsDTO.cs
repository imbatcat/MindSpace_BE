using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.DTOs.Tests
{
    public class CreateTestWithoutQuestionsDTO
    {
        public string Title { get; set; }
        public string? TestCode { get; set; }
        public TargetUser? TargetUser { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public int TestCategoryId { get; set; }
        public int SpecializationId { get; set; }

    }
}
