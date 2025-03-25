namespace MindSpace.Application.DTOs.Statistics.TestResponseStatistics
{
    public class RankGroupAnalysisDto
    {
        public int TestId { get; set; }
        public int? SchoolId { get; set; }
        public int TotalResponses { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<RankGroupDto> RankGroups { get; set; } = new List<RankGroupDto>();
    }
}
