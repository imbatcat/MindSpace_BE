using MindSpace.Domain.Entities.Tests;
using System.Globalization;

namespace MindSpace.Application.DTOs.Statistics.TestResponseStatistics
{
    public class RankGroupDto
    {
        public string Rank { get; set; }
        public int ResponseCount { get; set; }
        public double AverageScore { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public double MedianScore { get; set; }
        public double Percentage { get; set; }

        public static RankGroupDto MapToRankGroupDto(IGrouping<string, TestResponse> group)
        {
            string key = group.Key;
            var responses = group.ToList();
            var responseCount = responses.Count;
            var averageScore = responseCount > 0 ? Math.Round(responses.Average(x => x.TotalScore ?? 0),2) : 0;
            var minScore = responseCount > 0 ? responses.Min(x => x.TotalScore ?? 0) : 0;
            var maxScore = responseCount > 0 ? responses.Max(x => x.TotalScore ?? 0) : 0;
            var medianScore = responseCount > 0 ? CalculateMedian(responses) : 0;

            return new RankGroupDto
            {
                Rank = key,
                ResponseCount = responseCount,
                AverageScore = averageScore,
                MinScore = minScore,
                MaxScore = maxScore,
                MedianScore = medianScore
            };
        }
        private static double CalculateMedian(List<TestResponse> responses)
        {
            var sortedValues = responses.OrderBy(x => x.TotalScore).ToList();
            int count = sortedValues.Count;
            if (count == 0) return 0;

            int midIndex = count / 2;
            double result = count % 2 == 0
                ? (((double)(sortedValues[midIndex - 1].TotalScore ?? 0) + (double)(sortedValues[midIndex].TotalScore ?? 0)) / 2.0)
                : sortedValues[midIndex].TotalScore ?? 0;
            return Math.Round(result,2);
        }
    }

}
