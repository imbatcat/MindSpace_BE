namespace MindSpace.Domain.Entities.Tests
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }
        //Relationships
        public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new HashSet<TestQuestion>();

        // 1 question - M options
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new HashSet<QuestionOption>();
    }
}