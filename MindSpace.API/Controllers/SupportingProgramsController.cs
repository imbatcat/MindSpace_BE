using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.Helpers.Requests;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Specs;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.API.Controllers
{
    public class SupportingProgramsController : BaseApiController
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMediator _mediator;

        // ====================================
        // === Constructors
        // ====================================

        public SupportingProgramsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ====================================
        // === GET
        // ====================================

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SupportingProgram>>> GetSupportingPrograms(
            [FromQuery] SupportingProgramSpecParams specParams)
        {
            var supportPrograms = await _mediator.Send(new GetSupportingProgramsQuery(specParams));
            return Ok(supportPrograms);
        }

        // ====================================
        // === CREATE, PATCH, DELETE
        // ====================================
    }
}
