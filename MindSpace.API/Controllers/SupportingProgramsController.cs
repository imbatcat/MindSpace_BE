using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetCountSupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById;
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

        /// <summary>
        /// Get Supporting Programs By Params and Support Pagination
        /// </summary>
        /// <param name="specParams"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Supporting Program By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IReadOnlyList<SupportingProgram>>> GetSupportingProgramById(
            [FromQuery] int id)
        {
            var supportProgram = await _mediator.Send(new GetSupportingProgramByIdQuery(id));
            return Ok(supportProgram);
        }

        // ====================================
        // === CREATE, PATCH, DELETE
        // ====================================
    }
}
