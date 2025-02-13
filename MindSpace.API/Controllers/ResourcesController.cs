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

        // ====================================
        // === Constructors
        // ====================================

        public ResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ====================================
        // === QUERIES
        // ====================================


        // ====================================
        // === COMMANDS
        // ====================================

        [HttpPost("blog")]
        public async Task<ActionResult> CreateBlog([FromBody] string blogDraftId)
        {
            var blogDraft = await _blogDraftService.GetBlogDraftAsync(blogDraftId);

            // Check each field in the blog draft to see any missing data
            if (blogDraft == null) throw new NotFoundException(nameof(BlogDraft), blogDraftId);

            // Add blog to table
            var blogToAdd = _mapper.Map<BlogDraft, Resource>(blogDraft);
            _unitOfWork.Repository<Resource>().Insert(blogToAdd);

            // Add Blog Section to table
            foreach (var sectionDraft in blogDraft.Sections)
            {
                var sectionToAdd = _mapper.Map<SectionDraft, ResourceSection>(sectionDraft);
                sectionToAdd.ResourceId = blogToAdd.Id;
                _unitOfWork.Repository<ResourceSection>().Insert(sectionToAdd);
            }

            await _unitOfWork.CompleteAsync();
            return Ok(new { Message = "Blog created successfully", ResourceId = blogToAdd.Id });
        }
    }
}
 