namespace MindSpace.Application.DTOs.Statistics
{
    public class CountOverviewDTO
    {
        public int TotalStudentsCount { get; set; }
        public int TotalTestsCount { get; set; }
        public int TotalSupportingProgramCount { get; set; }
        public int TotalResourcesCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
