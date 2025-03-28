using MediatR;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;

namespace MindSpace.Application.Features.Statistics.Query.GetTopPsychologistRevenueStatistics
{
    public class GetTopPsychologistRevenueQuery : IRequest<List<PsychologistRevenueStatDTO>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Top { get; set; } = 5;
        public string SortBy { get; set; } = "revenue";
    }
}
