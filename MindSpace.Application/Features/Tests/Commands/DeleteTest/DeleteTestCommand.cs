using MediatR;

namespace MindSpace.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommand : IRequest
    {
        public int TestId { get; set; }
    }
}
