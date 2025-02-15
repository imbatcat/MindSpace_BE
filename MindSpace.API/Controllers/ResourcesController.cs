using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsArticle;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog;

namespace MindSpace.API.Controllers
{
    public class ResourcesController : BaseApiController
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMediator _mediator;


        // ====================================
        // === Constructors
        // ====================================

        public ResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ====================================
        // === Queries
        // ====================================

        // GET: /resources/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResourceResponseDTO>> GetResourceById(
            [FromRoute] int id)
        {
            return null;
        }


        // ====================================
        // === Commands
        // ====================================

        // PUT: /resources/blog/blogdraft:13134
        [HttpPut("blog/{blogDraftId}")]
        public async Task<ActionResult> CreateBlog(
            [FromRoute] string blogDraftId)
        {
            await _mediator.Send(new CreateResourceAsBlogCommand(blogDraftId));
            return NoContent();
        }

        // POST: /resources/article
        [HttpPost("article")]
        public async Task<ActionResult> CreateArticle(
            [FromBody] CreatedResourceAsArticleCommand createdArticle)
        {
            await _mediator.Send(createdArticle);
            return NoContent();
        }
    }
}
