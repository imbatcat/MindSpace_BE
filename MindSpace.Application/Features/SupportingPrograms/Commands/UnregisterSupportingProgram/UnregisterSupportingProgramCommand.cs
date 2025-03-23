using MediatR;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.UnregisterSupportingProgram
{
    public class UnregisterSupportingProgramCommand : IRequest
    {
        public int StudentId { get; set; }
        public int SupportingProgramId { get; set; }
    }
}
