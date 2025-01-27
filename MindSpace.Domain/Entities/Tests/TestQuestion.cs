using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Domain.Entities.Tests
{
    public class TestQuestion : BaseEntity
    {
        public string Title { get; set; }
        public QuestionFormats QuestionFormat { get; set; }

        //Relationships
        public virtual ICollection<TestQuestionOption> TestTestQuestions { get; set; } = new HashSet<TestQuestionOption>();

        // 1 question - M options
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new HashSet<QuestionOption>();
    }
}