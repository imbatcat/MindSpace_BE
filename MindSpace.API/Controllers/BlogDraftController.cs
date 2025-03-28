using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Draft.Commands.DeleteBlogDraft;
using MindSpace.Application.Features.Draft.Commands.UpdateBlogDraft;
using MindSpace.Application.Features.Draft.Queries.GetBlogDraftById;
using MindSpace.Domain.Entities.Drafts.Blogs;

namespace MindSpace.API.Controllers;

[Route("api/v{version:apiVersion}/blog-draft")]
public class BlogDraftController(IMediator mediator) : BaseApiController
{
    // ==============================
    // === GET
    // ==============================

    // GET /api/v1/blog-draft/{id}
    // Get a blog draft by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<BlogDraft>> GetBlogDraftById([FromRoute] string id)
    {
        var blogDraft = await mediator.Send(new GetBlogDraftByIdQuery(id));
        return Ok(blogDraft);
    }

    // ==============================
    // === POST, PUT, DELETE, PATCH
    // ==============================

    // DELETE /api/v1/blog-draft/{id}
    // Delete a blog draft
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBlogDraft(
        [FromRoute] string id)
    {
        await mediator.Send(new DeleteBlogDraftCommand(id));
        return NoContent();
    }

    // POST /api/v1/blog-draft
    // Create or update a blog draft
    [HttpPost]
    public async Task<ActionResult<BlogDraft>> UpdateBlogDraft(
        [FromBody] BlogDraft blogDraft)
    {
        var updatedBlogDraft = await mediator.Send(new UpdateBlogDraftCommand(blogDraft));
        return Ok(updatedBlogDraft);
    }
}
