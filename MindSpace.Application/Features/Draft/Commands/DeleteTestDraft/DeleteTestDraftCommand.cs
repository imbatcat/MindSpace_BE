using MediatR;
namespace MindSpace.Application.Features.Draft.Commands.DeleteTestDraft
{
    public class DeleteTestDraftCommand : IRequest
    {
        public string Id { get; }

        public DeleteTestDraftCommand(string id)
        {
            Id = id;
        }
    }
}
