namespace MindSpace.Application.DTOs.Tests
{
    public class TestResponseResponseDTO : TestResponseOverviewResponseDTO
    {
        public ICollection<TestResponseItemResponseDTO> TestResponseItems { get; set; }
    }
}
