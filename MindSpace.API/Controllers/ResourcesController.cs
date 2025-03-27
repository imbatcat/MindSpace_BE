using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsArticle;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog;
using MindSpace.Application.Features.Resources.Commands.ToggleResourceStatus;
using MindSpace.Application.Features.Resources.Queries.GetArticles;
using MindSpace.Application.Features.Resources.Queries.GetBlogs;
using MindSpace.Application.Features.Resources.Queries.GetResourceAsArticleById;
using MindSpace.Application.Features.Resources.Queries.GetResourceAsBlogById;
using MindSpace.Application.Features.SupportingPrograms.Commands.ToggleSupportingProgramStatus;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.API.Controllers;

public class ResourcesController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/resources/articles/{id}
    [Cache(600)]
    [HttpGet("articles/{id:int}")]
    public async Task<ActionResult<ArticleResponseDTO>> GetResourceAsArticleById(
        [FromRoute] int id)
    {
        var article = await mediator.Send(new GetResourceAsArticleByIdQuery(id));
        return Ok(article);
    }

    // GET /api/resources/blogs/{id}
    [Cache(600)]
    [HttpGet("blogs/{id:int}")]
    public async Task<ActionResult<BlogResponseDTO>> GetResourceAsBlogById(
        [FromRoute] int id)
    {
        var blog = await mediator.Send(new GetResourceAsBlogByIdQuery(id));
        return Ok(blog);
    }

    // GET /api/resources/articles
    //[Cache(30000)]
    [HttpGet("articles")]
    public async Task<ActionResult<Pagination<ArticleResponseDTO>>> GetAllArticles(
        [FromQuery] ResourceSpecificationSpecParams specParams)
    {
        var articles = await mediator.Send(new GetArticlesQuery(specParams));
        return PaginationOkResult(articles.Data, articles.Count, specParams.PageIndex, specParams.PageSize);
    }

    // GET /api/resources/blogs
    //[Cache(30000)]
    [HttpGet("blogs")]
    public async Task<ActionResult<Pagination<BlogResponseDTO>>> GetAllBlogs(
        [FromQuery] ResourceSpecificationSpecParams specParams)
    {
        var blogs = await mediator.Send(new GetBlogsQuery(specParams));
        return PaginationOkResult(blogs.Data, blogs.Count, specParams.PageIndex, specParams.PageSize);
    }

    // ==============================
    // === POST, PUT, DELETE, PATCH
    // ==============================

    // POST /api/resources/blogs
    [InvalidateCache("/api/resources/blogs|")]
    [HttpPost("blogs")]
    public async Task<ActionResult> CreateBlog(
        [FromBody] CreateResourceAsBlogCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetResourceAsBlogById), new { result.Id }, null);
    }

    // POST /api/resources/articles
    [InvalidateCache("/api/resources/blogs|")]
    [HttpPost("articles")]
    public async Task<ActionResult> CreateArticle(
        [FromBody] CreatedResourceAsArticleCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetResourceAsArticleById), new { result.Id }, null);
    }

    [InvalidateCache("/api/resources|")]
    [HttpPut("{id}/toggle-status")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SchoolManager}")]
    public async Task<IActionResult> ToggleResourceStatus([FromRoute] int id)
    {
        await mediator.Send(new ToggleResourceStatusCommand { Id = id });
        return NoContent();
    }
}
