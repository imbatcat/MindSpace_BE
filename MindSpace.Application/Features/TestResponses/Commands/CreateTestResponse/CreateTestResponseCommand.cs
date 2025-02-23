using MediatR;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.TestResponses.Commands.CreateTestResponse
{
    public class CreateTestResponseCommand : IRequest<TestResponseOverviewResponseDTO>
    {
        public int? TotalScore { get; set; }
        public string? TestScoreRankResult { get; set; }
        public int? StudentId { get; set; }
        public int? ParentId { get; set; }
        public int TestId { get; set; }
        public ICollection<TestResponseItemResponseDTO> TestResponseItems { get; set; } = new HashSet<TestResponseItemResponseDTO>();
    }
}
