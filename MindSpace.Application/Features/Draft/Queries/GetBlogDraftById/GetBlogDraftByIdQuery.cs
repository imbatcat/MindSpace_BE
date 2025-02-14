using MediatR;
using MindSpace.Domain.Entities.Drafts.Blogs;

namespace MindSpace.Application.Features.Draft.Queries.GetBlogDraftById
{
    public class GetBlogDraftByIdQuery : IRequest<BlogDraft>
    {
        public string Id { get; private set; }

        public GetBlogDraftByIdQuery(string id)
        {
            Id = id;
        }
    }
}
