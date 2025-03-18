using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Questions.Queries.GetQuestions;
using MindSpace.Application.Specifications.QuestionSpecifications;

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
        public async Task<ActionResult<IReadOnlyList<QuestionResponseDTO>>> GetQuestions(
            [FromQuery] QuestionSpecParams specParams)
        {
            var questionDtos = await mediator.Send(new GetQuestionsQuery(specParams));

            return PaginationOkResult<QuestionResponseDTO>(
                questionDtos.Data,
                questionDtos.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }
    }
}
