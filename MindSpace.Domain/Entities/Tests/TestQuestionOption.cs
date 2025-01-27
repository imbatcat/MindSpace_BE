namespace MindSpace.Domain.Entities.Tests
{
    public class TestQuestionOption
    {
        //Relationships
        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        public int TestQuestionId { get; set; }
        public virtual TestQuestion TestQuestion { get; set; }

        public int QuestionOptionId { get; set; }
        public virtual QuestionOption QuestionOption { get; set; }
    }
}