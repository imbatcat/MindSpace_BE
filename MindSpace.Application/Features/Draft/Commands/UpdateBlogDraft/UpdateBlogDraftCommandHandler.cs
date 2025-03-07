using MediatR;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Draft.Commands.UpdateBlogDraft;

public class UpdateBlogDraftCommandHandler(IBlogDraftService blogDraftService) : IRequestHandler<UpdateBlogDraftCommand, BlogDraft>
{
    public async Task<BlogDraft> Handle(UpdateBlogDraftCommand request, CancellationToken cancellationToken)
    {
        var updatedBlogDraft = await blogDraftService.SetBlogDraftAsync(request.BlogDraft)
            ?? throw new NotFoundException(nameof(BlogDraft), request.BlogDraft.Id);

        return updatedBlogDraft;
    }
}
