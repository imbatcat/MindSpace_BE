using MediatR;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetTestResponseRankAnalysisStatistics
{
    public class GetTestResponseRankAnalysisStatisticsQuery : IRequest<RankGroupAnalysisDto>
    {
        public required int TestId { get; set; }
        public int? SchoolId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
