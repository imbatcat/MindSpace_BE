using MediatR;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Draft.Commands.UpdateBlogDraft
{
    public class UpdateBlogDraftCommandHandler : IRequestHandler<UpdateBlogDraftCommand, BlogDraft>
    {
        private readonly IBlogDraftService _blogDraftService;

        public UpdateBlogDraftCommandHandler(IBlogDraftService blogDraftService)
        {
            _blogDraftService = blogDraftService;
        }

        public async Task<BlogDraft> Handle(UpdateBlogDraftCommand request, CancellationToken cancellationToken)
        {
            var updatedBlogDraft = await _blogDraftService.SetBlogDraftAsync(request.BlogDraft)
                ?? throw new NotFoundException(nameof(BlogDraft), request.BlogDraft.Id);

            return updatedBlogDraft;
        }
    }
}
