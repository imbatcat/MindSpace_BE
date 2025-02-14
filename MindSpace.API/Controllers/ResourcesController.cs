using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsBlog;
using MindSpace.Application.UserContext;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;

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

        public async Task<ActionResult<IReadOnlyList<>>>

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
