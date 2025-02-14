using MediatR;
using Microsoft.AspNetCore.Mvc;
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



        // ====================================
        // === Commands
        // ====================================

        [HttpPut("blog/{blogDraftId}")]
        public async Task<ActionResult> CreateBlog([FromRoute] string blogDraftId)
        {
            await _mediator.Send(new CreateResourceAsBlogCommand(blogDraftId));
            return NoContent();
        }
    }
}
