using MediatR;
using MindSpace.Application.DTOs.Tests;

namespace MindSpace.Application.Features.TestResponses.Queries.GetTestScoreRankByTotalScore
{
    public class GetTestScoreRankByTotalScoreQuery : IRequest<TestScoreRankResponseDTO>
    {
        public int TotalScore { get; set; }
        public int TestId { get; set; }
        public GetTestScoreRankByTotalScoreQuery(int totalScore, int testId)
        {
            TotalScore = totalScore;
            TestId = testId;
        }
    }
}
