using MediatR;
using MindSpace.Application.DTOs.Statistics;

namespace MindSpace.Application.Features.Statistics.Query.GetOverviewStatistics
{
    public class GetOverviewStatisticsQuery : IRequest<CountSchoolOverviewDataDTO>
    {
        public int SchoolId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
