using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.SupportingProgramHistories.Queries.GetCountSupportingProgramHistories;
using MindSpace.Application.Features.SupportingProgramHistories.Queries.GetSupportingProgramHistories;
using MindSpace.Application.Specifications.SupportingProgramHistory;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.API.Controllers
{
    public class SupportingProgramHistoriesController : BaseApiController
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMediator _mediator;

        // ====================================
        // === Constructors
        // ====================================

        public SupportingProgramHistoriesController(IMediator mediator)
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
        public async Task<ActionResult<IReadOnlyList<SupportingProgramHistory>>> GetSupportingProgramHistories(
            [FromQuery] SupportingProgramHistorySpecParams specParams)
        {
            var supportProgramHistories = await _mediator.Send(new GetSupportingProgramHistoriesQuery(specParams));
            var count = await _mediator.Send(new GetCountSupportingProgramHistoryQuery(specParams));

            return PaginationOkResult<SupportingProgramHistory>(
                supportProgramHistories,
                count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }
    }
}
