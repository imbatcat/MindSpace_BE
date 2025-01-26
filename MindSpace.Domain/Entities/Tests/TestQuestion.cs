using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Domain.Entities.Tests
{
    public class TestQuestion : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public QuestionFormats QuestionFormat { get; set; }

        //Relationships
        public virtual ICollection<TestTestQuestion> TestTestQuestions { get; set; } = new HashSet<TestTestQuestion>();

        // 1 question - M options
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new HashSet<QuestionOption>();
    }
}