using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs;
using MindSpace.Application.Features.Specializations.Queries;
using MindSpace.Application.Specifications.SpecializationSpecifications;

namespace MindSpace.API.Controllers
{

    public class SpecializationsController(
        IMediator mediator) : BaseApiController
    {
        // ====================================
        // === GET
        // ====================================

        // GET /api/specializations
        [Cache(30000)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SpecializationDTO>>> GetQuestions(
            [FromQuery] SpecializationSpecParams specParams)
        {
            var specializationDtos = await mediator.Send(new GetSpecializationsQuery(specParams));

            return PaginationOkResult<SpecializationDTO>(
                specializationDtos.Data,
                specializationDtos.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }
    }
}
