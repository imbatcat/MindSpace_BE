using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetCountSupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.API.Controllers
{
    [Authorize]
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
            var count = await _mediator.Send(new GetCountSupportingProgramsQuery(specParams));

            return PaginationOkResult<SupportingProgram>(
                supportPrograms,
                count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }

        // ====================================
        // === CREATE, PATCH, DELETE
        // ====================================
    }
}
