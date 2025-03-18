namespace MindSpace.Application.DTOs.Statistics.TestResponseStatistics.TestQuestionResponseStatistics
{
    public class TestQuestionResponseStatisticsAnalysisDto
    {
        public int TestId { get; set; }
        public int? SchoolId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // statistics for each question
        public List<TestQuestionResponseStatisticsDto> TestQuestionResponseStatistics { get; set; } = new List<TestQuestionResponseStatisticsDto>();
    }
}
