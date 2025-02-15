using AutoMapper;
using MediatR;
using MindSpace.Application.UserContext;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog
{
    public class CreateResourceAsBlogCommandHandler : IRequestHandler<CreateResourceAsBlogCommand>
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IBlogDraftService _blogDraftService;
        private readonly IUnitOfWork _unitOfWork;

        // ====================================
        // === Constructors
        // ====================================

        public CreateResourceAsBlogCommandHandler(
            IUserContext userContext,
            IMapper mapper,
            IBlogDraftService blogDraftService,
            IUnitOfWork unitOfWork)
        {
            _userContext = userContext;
            _mapper = mapper;
            _blogDraftService = blogDraftService;
            _unitOfWork = unitOfWork;
        }

        // ====================================
        // === Methods
        // ====================================

        public async Task Handle(CreateResourceAsBlogCommand request, CancellationToken cancellationToken)
        {
            var blogDraft = await _blogDraftService.GetBlogDraftAsync(request.BlogDraftId);

            // Check each field in the blog draft to see any missing data
            if (blogDraft == null) throw new NotFoundException(nameof(BlogDraft), request.BlogDraftId);

            // Add blog to table
            var blogToAdd = _mapper.Map<BlogDraft, Resource>(blogDraft);

            // Add blog Section to table
            foreach (var sectionDraft in blogDraft.Sections)
            {
                var sectionToAdd = _mapper.Map<SectionDraft, ResourceSection>(sectionDraft);
                sectionToAdd.Resource = blogToAdd;
                blogToAdd.ResourceSections.Add(sectionToAdd);
            }

            // Commit all changes
            _unitOfWork.Repository<Resource>().Insert(blogToAdd);
            await _unitOfWork.CompleteAsync();
        }
    }
}





