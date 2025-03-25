using MediatR;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics.TestQuestionResponseStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetTestQuestionResponseStatistics
{
    public class GetTestQuestionResponseStatisticsQuery : IRequest<TestQuestionResponseStatisticsAnalysisDto>
    {
        public required int TestId { get; set; }
        public int? SchoolId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
