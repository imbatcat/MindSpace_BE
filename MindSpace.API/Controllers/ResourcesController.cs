using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsArticle;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog;
using MindSpace.Application.Features.Resources.Queries.GetResourceAsArticleById;
using MindSpace.Application.Features.Resources.Queries.GetResourceAsBlogById;
using MindSpace.Application.Specifications.ResourceSpecifications;

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

        // GET: /resources/article/1
        [HttpGet("articles/{id:int}")]
        public async Task<ActionResult<ArticleResponseDTO>> GetResourceAsArticleById(
            [FromRoute] int id)
        {
            var article = await _mediator.Send(new GetResourceAsArticleByIdQuery(id));
            return Ok(article);
        }

        // GET: /resources/blogs/1
        [HttpGet("blogs/{id:int}")]
        public async Task<ActionResult<BlogResponseDTO>> GetResourceAsBlogById(
            [FromRoute] int id)
        {
            var blog = await _mediator.Send(new GetResourceAsBlogByIdQuery(id));
            return Ok(blog);
        }

        [HttpGet]
        public async Task<ActionResult<ArticleResponseDTO>> GetAllArticles(
            [FromQuery] ResourceSpecificationSpecParams specParams)
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<BlogResponseDTO>> GetAllBlogs(
            [FromQuery] ResourceSpecificationSpecParams specParams)
        {
            return null;
        }

        // ====================================
        // === Commands
        // ====================================

        // PUT: /resources/blog/blogdraft:13134
        [HttpPut("blogs/{blogDraftId}")]
        public async Task<ActionResult> CreateBlog(
            [FromRoute] string blogDraftId)
        {
            await _mediator.Send(new CreateResourceAsBlogCommand(blogDraftId));
            return NoContent();
        }

        // POST: /resources/articles
        [HttpPost("articles")]
        public async Task<ActionResult> CreateArticle(
            [FromBody] CreatedResourceAsArticleCommand createdArticle)
        {
            await _mediator.Send(createdArticle);
            return NoContent();
        }
    }
}
