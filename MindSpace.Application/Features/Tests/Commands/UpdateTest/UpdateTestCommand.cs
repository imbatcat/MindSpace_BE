using MediatR;

namespace MindSpace.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommand : IRequest
    {
        public required int TestId { get; set; }
        public bool? IsPublished { get; set; } // for publish test
        public string? TestDraftId { get; set; }
        public bool IsModifiedQuestions { get; set; }
    }
}
