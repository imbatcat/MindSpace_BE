using MediatR;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetTestResponseTimeAnalysisStatistics
{
    public class GetTestResponseTimeAnalysisStatisticsQuery : IRequest<TimeGroupAnalysisDto>
    {
        public required int TestId { get; set; }
        public required string TimePeriod { get; set; }
        public int? SchoolId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
