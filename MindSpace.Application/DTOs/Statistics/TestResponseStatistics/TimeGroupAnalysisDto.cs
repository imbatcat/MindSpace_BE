namespace MindSpace.Application.DTOs.Statistics.TestResponseStatistics
{
    public class TimeGroupAnalysisDto
    {
        public int TestId { get; set; }
        public int? SchoolId { get; set; }
        public string? TimePeriod { get; set; }
        public List<TimeGroupDto> TimeGroups { get; set; } = new List<TimeGroupDto>();
    }
}
