using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Tests
{
    public class TestResponse : BaseEntity
    {
        public int? TotalScore { get; set; }
        public string? TestScoreRankResult { get; set; }

        //Relationships

        // 1 Student/Parent - M Test Responses
        public int? StudentId { get; set; }
        public int? ParentId { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Parent? Parent { get; set; }

        // 1 Test - M Test Responses

        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        // 1 Test Response - M Test Response Items
        public virtual ICollection<TestResponseItem> TestResponseItems { get; set; } = new HashSet<TestResponseItem>();
    }
}