using MediatR;
using MindSpace.Application.Services;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
