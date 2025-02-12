using MediatR;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Draft.Queries.GetBlogDraftById
{
    public class GetBlogDraftByIdQueryHandler : IRequestHandler<GetBlogDraftByIdQuery, BlogDraft>
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IBlogDraftService _blogDraftService;

        // ====================================
        // === Constructors
        // ====================================

        public GetBlogDraftByIdQueryHandler(IBlogDraftService blogDraftService)
        {
            _blogDraftService = blogDraftService;
        }

        // ====================================
        // === Methods
        // ====================================

        public async Task<BlogDraft> Handle(GetBlogDraftByIdQuery request, CancellationToken cancellationToken)
        {
            var blogDraft = await _blogDraftService.GetBlogDraftAsync(request.Id);

            // If null than return empty draft, but does not store in the redis
            return blogDraft ?? new BlogDraft() { Id = request.Id };
        }
    }
}
