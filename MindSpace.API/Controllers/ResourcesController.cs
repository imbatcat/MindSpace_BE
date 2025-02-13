using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.UserContext;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.API.Controllers
{
    public class ResourcesController : BaseApiController
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMediator _mediator;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBlogDraftService _blogDraftService;

        public ResourcesController(IMediator mediator, IUserContext userContext, IUnitOfWork unitOfWork, IMapper mapper, IBlogDraftService blogDraftService)
        {
            _mediator = mediator;
            _userContext = userContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _blogDraftService = blogDraftService;
        }

        // ====================================
        // === Constructors
        // ====================================



        // ====================================
        // === QUERIES
        // ====================================


        // ====================================
        // === COMMANDS
        // ====================================

        [HttpPost("blog/{blogDraftId}")]
        public async Task<ActionResult> CreateBlog([FromRoute] string blogDraftId)
        {
            var blogDraft = await _blogDraftService.GetBlogDraftAsync(blogDraftId);

            // Check each field in the blog draft to see any missing data
            if (blogDraft == null) throw new NotFoundException(nameof(BlogDraft), blogDraftId);

            // Add blog to table
            var blogToAdd = _mapper.Map<BlogDraft, Resource>(blogDraft);
            blogToAdd.ResourceSections = new List<ResourceSection>();

            // Add Blog Section to table
            foreach (var sectionDraft in blogDraft.Sections)
            {
                var sectionToAdd = _mapper.Map<SectionDraft, ResourceSection>(sectionDraft);
                sectionToAdd.Resource = blogToAdd;
                blogToAdd.ResourceSections.Add(sectionToAdd);
            }

            _unitOfWork.Repository<Resource>().Insert(blogToAdd);
            await _unitOfWork.CompleteAsync();
            return Ok(new { Message = "Blog created successfully", ResourceId = blogToAdd.Id });
        }
    }
}
