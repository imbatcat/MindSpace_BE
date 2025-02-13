namespace MindSpace.Application.DTOs.Tests
{
    public class TestResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? TestCode { get; set; }
        public string? TestCategoryId { get; set; } // may change to TestCategoryResponseDTO later if needed
        public string? SpecializationId { get; set; } // may change to SpecializationResponseDTO later if needed
        public string TargetUser { get; set; }
        public string? Description { get; set; }
        public int QuestionCount { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public ICollection<QuestionResponseDTO> Questions { get; set; }
        public ICollection<PsychologyTestOptionResponseDTO> PsychologyTestOptions { get; set; }
        public ICollection<TestScoreRankResponseDTO> TestScoreRanks { get; set; }
    }
}