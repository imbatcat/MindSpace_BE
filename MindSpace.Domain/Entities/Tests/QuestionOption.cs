namespace MindSpace.Domain.Entities.Tests
{
    public class QuestionOption : BaseEntity
    {
        // 1 Question - M Options
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        // Option Displayed Text
        public string DisplayedText { get; set; }

    }
}
