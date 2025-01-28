using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Tests
{
    public class Test : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int QuestionCount { get; set; }
        public decimal Price { get; set; }
        public TestStatus TestStatus { get; set; }

        //Relationships
        public int TestCategoryId { get; set; }
        public virtual TestCategory TestCategory { get; set; }
        public virtual ICollection<TestQuestionOption> TestTestQuestions { get; set; } = new HashSet<TestQuestionOption>();
        public virtual ICollection<TestResponse> TestResponses { get; set; } = new HashSet<TestResponse>();
        public int ManagerId { get; set; }
        public virtual Manager Manager { get; set; }
    }
}