using MediatR;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.RegisterSupportingProgram
{
    public class RegisterSupportingProgramCommand : IRequest
    {
        public int StudentId { get; set; }
        public int SupportingProgramId { get; set; }
    }
}
