using MindSpace.Application.DTOs.ApplicationUsers;

namespace MindSpace.Application.DTOs.Tests
{
    public class TestResponseOverviewResponseDTO
    {
        public int Id { get; set; }
        public int? TotalScore { get; set; }
        public string? TestScoreRankResult { get; set; }
        public StudentResponseDTO? Student { get; set; }
        public ApplicationUserResponseDTO? Parent { get; set; }
        public TestOverviewResponseDTO Test { get; set; }

    }
}
