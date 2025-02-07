namespace MindSpace.Domain.Entities.Tests
{
    // Store history answer of student for each question
    public class TestResponseItem : BaseEntity
    {
        // 1 Test Response - M Test Response Items
        public int TestResponseId { get; set; }
        public TestResponse TestResponse { get; set; }

        // Question Content
        public string QuestionContent { get; set; }
        public int? Score { get; set; }

        // Field text to store displayed text of the answer
        public string AnswerText { get; set; }

    }
}
