using MediatR;
using MindSpace.Application.DTOs.Resources;

namespace MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog
{
    public class CreateResourceAsBlogCommand : IRequest<BlogResponseDTO>
    {
        public string BlogDraftId { get; set; }

        public CreateResourceAsBlogCommand(string blogDraftId)
        {
            BlogDraftId = blogDraftId;
        }
    }
}
