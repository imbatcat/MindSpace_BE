using MediatR;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetRevenueTimeAnalysisStatistics
{
    public class GetRevenueTimeAnalysisStatisticsQuery : IRequest<List<RevenueStatDTO>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string GroupBy { get; set; } = "month";
    }
}
