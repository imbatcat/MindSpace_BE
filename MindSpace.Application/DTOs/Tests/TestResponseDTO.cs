namespace MindSpace.Application.DTOs.Tests
{
    public class TestResponseDTO : TestOverviewResponseDTO
    {
        public ICollection<QuestionResponseDTO> Questions { get; set; }
        public ICollection<PsychologyTestOptionResponseDTO> PsychologyTestOptions { get; set; }
        public ICollection<TestScoreRankResponseDTO> TestScoreRanks { get; set; }
    }
}