using MediatR;

namespace MindSpace.Application.Features.Resources.Commands.ToggleResourceStatus
{
    public class ToggleResourceStatusCommand : IRequest
    {
        public int Id { get; set; }
    }
}
