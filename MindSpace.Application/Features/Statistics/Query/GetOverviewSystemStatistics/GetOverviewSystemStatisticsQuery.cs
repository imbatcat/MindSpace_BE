using MediatR;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetOverviewSystemStatistics
{
    public class GetOverviewSystemStatisticsQuery : IRequest<OverviewStatisticsDTO>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
