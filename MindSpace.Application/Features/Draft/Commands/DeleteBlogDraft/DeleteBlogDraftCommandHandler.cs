using MediatR;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Draft.Commands.DeleteBlogDraft
{
    public class DeleteBlogDraftCommandHandler : IRequestHandler<DeleteBlogDraftCommand>
    {
        private readonly IBlogDraftService _blogDraftService;

        public DeleteBlogDraftCommandHandler(IBlogDraftService blogDraftService)
        {
            _blogDraftService = blogDraftService;
        }

        public async Task Handle(DeleteBlogDraftCommand request, CancellationToken cancellationToken)
        {
            var isDeletedSuccessful = await _blogDraftService.DeleteBlogDraftAsync(request.Id);

            if (!isDeletedSuccessful) throw new NotFoundException(nameof(BlogDraft), request.Id);
        }
    }
}
