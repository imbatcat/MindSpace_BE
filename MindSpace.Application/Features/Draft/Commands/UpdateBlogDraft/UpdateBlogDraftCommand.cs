using MediatR;
using MindSpace.Domain.Entities.Drafts.Blogs;

namespace MindSpace.Application.Features.Draft.Commands.UpdateBlogDraft
{
    public class UpdateBlogDraftCommand : IRequest<BlogDraft>
    {
        public BlogDraft BlogDraft { get; }

        public UpdateBlogDraftCommand(BlogDraft blogDraft)
        {
            BlogDraft = blogDraft;
        }
    }
}
