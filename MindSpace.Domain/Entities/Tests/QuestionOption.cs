using MindSpace.Domain.Entities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Tests
{
    public class QuestionOption : BaseEntity
    {
        // 1 Test Question - M Options
        public int? TestQuestionId { get; set; }
        public TestQuestion? TestQuestion { get; set; }

        // Option Displayed Text
        public string? OptionText { get; set; }

        // Option Score value
        public int? Score { get; set; }

        // Type of Fixed Format
        public FixedType? FixedType { get; set; }

        // Discriminator
        public string? Discriminator { get; set; }

        public virtual ICollection<TestQuestionOption> TestQuestionOptions { get; set; } = new HashSet<TestQuestionOption>();

        // 1 Option - M Test Response Items (1 options being chose M times)
        public virtual ICollection<TestResponseItem> TestResponseItems { get; set; } = new HashSet<TestResponseItem>();

    }
    }
}
