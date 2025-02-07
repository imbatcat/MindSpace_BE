using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Tests
{
    public class Test : BaseEntity
    {
        public string Title { get; set; }
        public string? TestCode { get; set; }
        public TargetUser TargetUser { get; set; }
        public string? Description { get; set; }
        public int QuestionCount { get; set; }
        public decimal Price { get; set; }
        public TestStatus TestStatus { get; set; }

        //Relationships

        // 1 Test Category - M Tests
        public int TestCategoryId { get; set; }
        public TestCategory TestCategory { get; set; }

        // 1 Author (Admin/Manager) - M Tests
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        // 1 Specialization - M Tests
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        // 1 Test - M Test Questions
        public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new HashSet<TestQuestion>();

        // 1 Test - M Test Publications
        public virtual ICollection<TestPublication> TestPublications { get; set; } = new HashSet<TestPublication>();
        // 1 Test - M Test Score Ranks
        public virtual ICollection<TestScoreRank> TestScoreRanks { get; set; } = new HashSet<TestScoreRank>();
        // 1 Test - M Psychology Test Options
        public virtual ICollection<PsychologyTestOption> PsychologyTestOptions { get; set; } = new HashSet<PsychologyTestOption>();
    }
}