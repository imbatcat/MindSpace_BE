namespace MindSpace.Domain.Entities.Tests
{
    public class TestScoreRank : BaseEntity
    {
        // 1 Specilization - M Test Score Ranks
        public int SpecilizationId { get; set; }
        public Specialization Specialization { get; set; }

        // Min Score and Max Score of a rank
        public int MinScore { get; set; }
        public int MaxScore { get; set; }

        // Displayed Text Result of a rank
        public string Result { get; set; }
    }
}
