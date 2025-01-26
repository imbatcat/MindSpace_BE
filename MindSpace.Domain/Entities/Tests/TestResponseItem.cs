using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Tests
{
    public class TestResponseItem : BaseEntity
    {
        // 1 Test Response - M Test Response Items
        public int TestResponseId { get; set; }
        public TestResponse TestResponse { get; set; }

        // 1 Test Response Item - 1 Selected Question Option
        public int? SelectedOptionId { get; set; }
        public QuestionOption?  SelectedOption { get; set; }

        // Nullable score and text for special answers
        public int? Score { get; set; }
        public int? AnswerText { get; set; }

    }
}
