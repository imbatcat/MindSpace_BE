using MediatR;
using MindSpace.Application.DTOs.Tests;

namespace MindSpace.Application.Features.TestResponses.Commands.CreateTestResponse
{
    public class CreateTestResponseCommand : IRequest<TestResponseOverviewResponseDTO>
    {
        public int? TotalScore { get; set; }
        public string? TestScoreRankResult { get; set; }
        public int? StudentId { get; set; }
        public int? ParentId { get; set; }
        public int TestId { get; set; }
        public ICollection<TestResponseItemRequestDTO> TestResponseItems { get; set; } = new HashSet<TestResponseItemRequestDTO>();
    }
}
