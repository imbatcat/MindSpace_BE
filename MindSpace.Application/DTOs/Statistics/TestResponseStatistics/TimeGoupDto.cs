using MindSpace.Domain.Entities.Tests;
using System.Globalization;

namespace MindSpace.Application.DTOs.Statistics.TestResponseStatistics
{
    public class TimeGroupDto
    {
        public string TimePeriod { get; set; }
        public int ResponseCount { get; set; }
        public double AverageScore { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public DateTime Date { get; set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public string MonthName { get; private set; }

        public static TimeGroupDto MapToTimeGroupDto(IGrouping<string, TestResponse> group, string timePeriod)
        {
            string key = group.Key;
            DateTime date;
            try
            {
                switch (timePeriod?.ToLower() ?? "day")
                {
                    case "day":
                        date = DateTime.ParseExact(key, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        break;
                    case "month":
                        date = DateTime.ParseExact(key + "-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        break;
                    case "year":
                        date = DateTime.ParseExact(key + "-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        break;
                    default:
                        date = DateTime.ParseExact(key, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        break;
                }
            }
            catch (FormatException)
            {
                date = DateTime.Today;
            }

            var responses = group.ToList();
            var responseCount = responses.Count;
            var averageScore = responseCount > 0 ? responses.Average(x => x.TotalScore ?? 0) : 0;
            var minScore = responseCount > 0 ? responses.Min(x => x.TotalScore ?? 0) : 0;
            var maxScore = responseCount > 0 ? responses.Max(x => x.TotalScore ?? 0) : 0;

            return new TimeGroupDto
            {
                TimePeriod = key,
                ResponseCount = responseCount,
                AverageScore = averageScore,
                MinScore = minScore,
                MaxScore = maxScore,
                Date = date,
                Year = date.Year,
                Month = date.Month,
                Day = date.Day,
                MonthName = date.ToString("MMMM", CultureInfo.InvariantCulture)
            };
        }
    }
}
