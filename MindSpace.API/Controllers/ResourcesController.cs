using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsArticle;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog;
using MindSpace.Application.Features.Resources.Queries.GetArticles;
using MindSpace.Application.Features.Resources.Queries.GetBlogs;
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

        // GET: /resources/articles
        [HttpGet("articles")]
        public async Task<ActionResult<Pagination<ArticleResponseDTO>>> GetAllArticles(
            [FromQuery] ResourceSpecificationSpecParams specParams)
        {
            var articles = await _mediator.Send(new GetArticlesQuery(specParams));
            return PaginationOkResult(articles.Data, articles.Count, specParams.PageIndex, specParams.PageSize);
        }

        // GET: /resources/blogs
        [HttpGet("blogs")]
        public async Task<ActionResult<Pagination<BlogResponseDTO>>> GetAllBlogs(
            [FromQuery] ResourceSpecificationSpecParams specParams)
        {
            var blogs = await _mediator.Send(new GetBlogsQuery(specParams));
            return PaginationOkResult(blogs.Data, blogs.Count, specParams.PageIndex, specParams.PageSize);
        }

        // ====================================
        // === Commands
        // ====================================

        // PUT: /resources/blogs/blogdraft:13134
        [HttpPost("blogs")]
        public async Task<ActionResult> CreateBlog(
            [FromBody] CreateResourceAsBlogCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetResourceAsBlogById), new { result.Id }, null);
        }

        // POST: /resources/articles
        [HttpPost("articles")]
        public async Task<ActionResult> CreateArticle(
            [FromBody] CreatedResourceAsArticleCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetResourceAsArticleById), new { result.Id}, null);
        }
    }
}
