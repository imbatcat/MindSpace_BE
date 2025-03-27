using MediatR;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.ToggleSupportingProgramStatus
{
    public class ToggleSupportingProgramStatusCommand : IRequest
    {
        public int Id { get; set; }
    }
}
