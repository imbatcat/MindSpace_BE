using MindSpace.Application.Specifications;

namespace MindSpace.Application.Features.SupportingProgramHistories.Specifications
{
    public class SupportingProgramHistorySpecParams : BasePagingParams
    {
        public int? StudentId { get; set; }
        public int? SupportingProgramId { get; set; }

        // Filter by Date Range
        public DateTime? JoinedAtForm { get; set; }
        public DateTime? JoinedAtTo { get; set; }

        public string? Sort { get; set; }
    }
}
