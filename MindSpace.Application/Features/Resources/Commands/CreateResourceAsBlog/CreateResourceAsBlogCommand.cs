using MediatR;

namespace MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog
{
    public class CreateResourceAsBlogCommand : IRequest
    {
        public string BlogDraftId { get; private set; }

        public CreateResourceAsBlogCommand(string blogDraftId)
        {
            BlogDraftId = blogDraftId;
        }
    }
}
