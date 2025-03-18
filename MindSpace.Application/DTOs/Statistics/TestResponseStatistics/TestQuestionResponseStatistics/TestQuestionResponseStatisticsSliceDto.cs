namespace MindSpace.Application.DTOs.Statistics.TestResponseStatistics.TestQuestionResponseStatistics
{
    public class TestQuestionResponseStatisticsSliceDto
    {
        public string AnswerOption { get; set; } // Maps to AnswerText
        public int Count { get; set; }
        public double Percentage { get; set; }
    }
}
