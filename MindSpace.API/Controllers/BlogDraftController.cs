using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.Draft.Commands.DeleteBlogDraft;
using MindSpace.Application.Features.Draft.Commands.UpdateBlogDraft;
using MindSpace.Application.Features.Draft.Queries.GetBlogDraftById;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.API.Controllers
{
    [Route("api/v{v:apiVersion}/blog-draft")]
    public class BlogDraftController : BaseApiController
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMediator _mediator;

        // ====================================
        // === Constructors
        // ====================================

        public BlogDraftController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ====================================
        // === GET
        // ====================================

        /// <summary>
        /// Get a blog draft or we create a blog draft
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BlogDraft>> GetBlogDraftById([FromRoute] string id)
        {
            var blogDraft = await _mediator.Send(new GetBlogDraftByIdQuery(id));
            return Ok(blogDraft);
        }

        // ====================================
        // === COMMANDS
        // ====================================

        /// <summary>
        /// Delete a Blog Draft
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBlogDraft(
            [FromRoute] string id)
        {
            await _mediator.Send(new DeleteBlogDraftCommand(id));
            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogDraft"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpPost]
        public async Task<ActionResult<BlogDraft>> UpdateBlogDraft(
            [FromBody] BlogDraft blogDraft)
        {
            // Set or update new
            var updatedBlogDraft = await _mediator.Send(new UpdateBlogDraftCommand(blogDraft));

            return Ok(updatedBlogDraft);
        }
    }
}
