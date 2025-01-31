namespace MindSpace.Domain.Entities.Tests
{
    public class QuestionCategory : BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new HashSet<QuestionOption>();
    }
}
