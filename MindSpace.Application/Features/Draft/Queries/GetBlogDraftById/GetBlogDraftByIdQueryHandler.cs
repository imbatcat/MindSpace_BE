using MediatR;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.Blogs;

namespace MindSpace.Application.Features.Draft.Queries.GetBlogDraftById;

public class GetBlogDraftByIdQueryHandler(IBlogDraftService blogDraftService) : IRequestHandler<GetBlogDraftByIdQuery, BlogDraft>
{
    public async Task<BlogDraft> Handle(GetBlogDraftByIdQuery request, CancellationToken cancellationToken)
    {
        var blogDraft = await blogDraftService.GetBlogDraftAsync(request.Id);

        // If null than return empty draft, but does not store in the redis
        return blogDraft ?? new BlogDraft() { Id = request.Id };
    }
}
