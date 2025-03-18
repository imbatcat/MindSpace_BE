namespace MindSpace.Application.DTOs.Statistics.TestResponseStatistics.TestQuestionResponseStatistics
{
    public class TestQuestionResponseStatisticsDto
    {
        public string QuestionContent { get; set; }

        // statistics of each option
        public List<TestQuestionResponseStatisticsSliceDto> Slices { get; set; } = new List<TestQuestionResponseStatisticsSliceDto>();
    }
}
