using MediatR;

namespace MindSpace.Application.Features.Draft.Commands.DeleteBlogDraft
{
    public class DeleteBlogDraftCommand : IRequest
    {
        public string Id { get; }

        public DeleteBlogDraftCommand(string id)
        {
            Id = id;
        }
    }
}
