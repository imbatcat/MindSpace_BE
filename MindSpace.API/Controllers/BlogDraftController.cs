using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Domain.Entities.Drafts.Blog;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.API.Controllers
{
    [Route("api/v{v:apiVersion}/blog-draft")]
    public class BlogDraftController : BaseApiController
    {
        public readonly IBlogDraftService _blogDraftService;

        public BlogDraftController(IBlogDraftService blogDraftService)
        {
            _blogDraftService = blogDraftService;
        }

        /// <summary>
        /// Get a blog draft or we create a blog draft
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BlogDraft>> GetOrCreateBlogDraft([FromRoute] string id)
        {
            var blogDraft = await _blogDraftService.GetBlogDraftAsync(id);

            // If dont have a draft, then simply return new draft but not yet
            // add to the redis
            return Ok(blogDraft ?? new BlogDraft() { Id = id });
        }

        /// <summary>
        /// Delete a Blog Draft
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBlogDraft(
            [FromRoute] string id)
        {
            var result = await _blogDraftService.DeleteBlogDraftAsync(id);

            if (!result) return BadRequest("Problem when deleting cart");

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
            var updatedBlogDraft = await _blogDraftService.SetBlogDraftAsync(blogDraft);

            // If not found then throw exception
            if (updatedBlogDraft == null) throw new NotFoundException(nameof(BlogDraft), blogDraft.Id);

            return Ok(updatedBlogDraft);
        }
    }
}
