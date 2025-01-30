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

        // 1 Question Category - M Options
        public int? QuestionCategoryId { get; set; }
        public QuestionCategory? QuestionCategory { get; set; }

        // Is Text Area
        //public bool? IsTextArea { get; set; }

        // Option Displayed Text
        public string OptionText { get; set; }

        // Option Score value
        public int Score { get; set; }
        
        public virtual ICollection<TestQuestionOption> TestQuestionOptions { get; set; } = new HashSet<TestQuestionOption>();

    }
}
