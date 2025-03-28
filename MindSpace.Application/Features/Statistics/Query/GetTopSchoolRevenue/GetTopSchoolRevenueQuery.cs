using MediatR;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetTopSchoolRevenue
{
    public class GetTopSchoolRevenueQuery : IRequest<List<SchoolRevenueDTO>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Top { get; set; } = 5;
    }
}
