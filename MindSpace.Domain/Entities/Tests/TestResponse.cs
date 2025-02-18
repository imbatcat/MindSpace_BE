using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Tests
{
    public class TestResponse : BaseEntity
    {
        public int? TotalScore { get; set; }

        public string? TestScoreRankResult { get; set; }

        //Relationships

        // 1 Student/Parent - M Test Responses
        public int RespondentId { get; set; }  // Can be either StudentId or ParentId
        public virtual Student Student { get; set; }
        public virtual Parent Parent { get; set; }

        // 1 Test Publication - M Test Responses

        public int TestPublicationId { get; set; }
        public virtual TestPublication TestPublication { get; set; }

        // 1 Test Response - M Test Response Items
        public virtual ICollection<TestResponseItem> TestResponseItems { get; set; } = new HashSet<TestResponseItem>();
    }
}