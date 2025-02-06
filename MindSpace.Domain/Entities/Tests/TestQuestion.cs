namespace MindSpace.Domain.Entities.Tests
{
    public class TestQuestion
    {
        //Relationships
        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}