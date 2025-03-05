using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Specifications.TestScoreRankSpecifications
{
    public class TestScoreRankSpecification : BaseSpecification<TestScoreRank>
    {
        public TestScoreRankSpecification(int testId)
            : base(x => x.TestId.Equals(testId))
        {
        }
        public TestScoreRankSpecification(int testId, int totalScore)
            : base(x => x.TestId.Equals(testId)
                    && x.MinScore <= totalScore
                    && x.MaxScore >= totalScore)
        {
        }
    }
}
