using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Draft.Commands.DeleteBlogDraft;
using MindSpace.Application.Features.Draft.Commands.UpdateBlogDraft;
using MindSpace.Application.Features.Draft.Queries.GetBlogDraftById;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Exceptions;

namespace MindSpace.API.Controllers;

[Route("api/v{v:apiVersion}/blog-draft")]
public class BlogDraftController(IMediator mediator) : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<BlogDraft>> GetBlogDraftById([FromRoute] string id)
    {
        var blogDraft = await mediator.Send(new GetBlogDraftByIdQuery(id));
        return Ok(blogDraft);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteBlogDraft(
        [FromRoute] string id)
    {
        await mediator.Send(new DeleteBlogDraftCommand(id));
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<BlogDraft>> UpdateBlogDraft(
        [FromBody] BlogDraft blogDraft)
    {
        var updatedBlogDraft = await mediator.Send(new UpdateBlogDraftCommand(blogDraft));
        return Ok(updatedBlogDraft);
    }
}
