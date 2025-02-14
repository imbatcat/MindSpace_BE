using MediatR;
using MindSpace.Application.DTOs.Tests;

namespace MindSpace.Application.Features.Tests.Commands.CreateTestManual
{
    public class CreateTestManualCommand : IRequest<TestOverviewResponseDTO>
    {
        public required string TestDraftId { get; set; }
    }
}
