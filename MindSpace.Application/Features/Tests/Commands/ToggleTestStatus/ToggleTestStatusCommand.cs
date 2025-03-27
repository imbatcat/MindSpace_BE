using MediatR;

namespace MindSpace.Application.Features.Tests.Commands.ToggleTestStatus
{
    public class ToggleTestStatusCommand : IRequest
    {
        public int TestId { get; set; }
    }
}
