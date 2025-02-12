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

        private readonly IBlogDraftService _blogDraftService;
        private readonly IMediator _mediator;

        // ====================================
        // === Constructors
        // ====================================

        public BlogDraftController(IBlogDraftService blogDraftService, IMediator mediator)
        {
            _blogDraftService = blogDraftService;
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
            var isDeleteSuccesfully = await _mediator.Send(new DeleteBlogDraftCommand(id));

            if (!isDeleteSuccesfully) return BadRequest("Problem when deleting blog draft");

            return Ok();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogDraft"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpPost("{id:int}")]
        public async Task<ActionResult<BlogDraft>> UpdateBlogDraft(
            [FromRoute] string id,
            [FromBody] BlogDraft blogDraft)
        {
            // Set or update new
            var updatedBlogDraft = await _mediator.Send(new UpdateBlogDraftCommand(id, blogDraft));

            return Ok(updatedBlogDraft);
        }
    }
}
