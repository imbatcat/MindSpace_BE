namespace MindSpace.Application.DTOs.Tests
{
    public class TestScoreRankResponseDTO
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public string Result { get; set; }
    }
}