using MediatR;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetRevenueBySpecializationStatistics
{
    public class GetRevenueBySpecializationQuery : IRequest<List<SpecializationRevenueStatDTO>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
