using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Tests
{
    // Store history answer of student for each question
    public class TestResponseItem : BaseEntity
    {
        // 1 Test Response - M Test Response Items
        public int TestResponseId { get; set; }
        public TestResponse TestResponse { get; set; }
        
        // Question Content
        public string QuestionTitle { get; set; }

        // Field to mark if the question is a text question
        public bool IsTextArea { get; set; }
        public int Score { get; set; }

        // Field text to store text answer
        public int? AnswerText { get; set; }

    }
}
