namespace MindSpace.Application.Specifications.TestResponseSpecifications
{
    public class TestResponseSpecParams : BasePagingParams
    {
        public int? MaxTotalScore { get; set; }
        public int? MinTotalScore { get; set; }
        public string? TestScoreRankResult { get; set; }
        public int? StudentId { get; set; }
        public int? ParentId { get; set; }
        public int? TestId { get; set; }
        public string? Sort { get; set; }
    }
}
