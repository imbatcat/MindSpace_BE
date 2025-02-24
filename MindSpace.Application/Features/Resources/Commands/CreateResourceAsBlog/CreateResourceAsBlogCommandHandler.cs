using AutoMapper;
using MediatR;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog
{
    public class CreateResourceAsBlogCommandHandler : IRequestHandler<CreateResourceAsBlogCommand, BlogResponseDTO>
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMapper _mapper;
        private readonly IBlogDraftService _blogDraftService;
        private readonly IUnitOfWork _unitOfWork;

        // ====================================
        // === Constructors
        // ====================================

        public CreateResourceAsBlogCommandHandler(
            IMapper mapper,
            IBlogDraftService blogDraftService,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _blogDraftService = blogDraftService;
            _unitOfWork = unitOfWork;
        }

        // ====================================
        // === Methods
        // ====================================

        public async Task<BlogResponseDTO> Handle(CreateResourceAsBlogCommand request, CancellationToken cancellationToken)
        {
            var blogDraft = await _blogDraftService.GetBlogDraftAsync(request.BlogDraftId);

            // Check each field in the blog draft to see any missing data
            if (blogDraft == null) throw new NotFoundException(nameof(BlogDraft), request.BlogDraftId);

            // Add blog to table
            var blogToAdd = _mapper.Map<BlogDraft, Resource>(blogDraft);

            // Commit all changes
            var addedBlog = _unitOfWork.Repository<Resource>().Insert(blogToAdd)
                ?? throw new CreateFailedException("Blog");
            await _unitOfWork.CompleteAsync();

            // Remove blog draft from redis
            await _blogDraftService.DeleteBlogDraftAsync(blogDraft.Id);

            return _mapper.Map<Resource, BlogResponseDTO>(addedBlog);
        }
    }
}





