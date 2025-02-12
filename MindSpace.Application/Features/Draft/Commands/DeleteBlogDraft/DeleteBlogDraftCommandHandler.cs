using MediatR;
using MindSpace.Application.Services;
using MindSpace.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Draft.Commands.DeleteBlogDraft
{
    public class DeleteBlogDraftCommandHandler : IRequestHandler<DeleteBlogDraftCommand, bool>
    {
        private readonly IBlogDraftService _blogDraftService;

        public DeleteBlogDraftCommandHandler(IBlogDraftService blogDraftService)
        {
            _blogDraftService = blogDraftService;
        }

        public async Task<bool> Handle(DeleteBlogDraftCommand request, CancellationToken cancellationToken)
        {
            return await _blogDraftService.DeleteBlogDraftAsync(request.Id);
        }
    }
}
