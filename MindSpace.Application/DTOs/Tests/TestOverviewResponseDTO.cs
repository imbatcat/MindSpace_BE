using MindSpace.Application.DTOs.ApplicationUsers;

namespace MindSpace.Application.DTOs.Tests
{
    public class TestOverviewResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TestCode { get; set; }
        public TestCategoryDTO? TestCategory { get; set; } // may change to TestCategoryResponseDTO later if needed
        public SpecializationDTO? Specialization { get; set; } // may change to SpecializationResponseDTO later if needed
        public string TargetUser { get; set; }
        public string? Description { get; set; }
        public int QuestionCount { get; set; }
        public decimal Price { get; set; }
        public ApplicationUserResponseDTO Author { get; set; }

    }
}
