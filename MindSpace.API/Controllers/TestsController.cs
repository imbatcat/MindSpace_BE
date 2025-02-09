

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Questions.Queries.GetQuestions;
using MindSpace.Application.Features.Tests.Queries.GetTests;
using MindSpace.Application.Specifications.QuestionSpecifications;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.API.Controllers
{
    public class TestsController : BaseApiController
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IMediator _mediator;

        // ====================================
        // === Constructors
        // ====================================

        public TestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ====================================
        // === GET
        // ====================================

        /// <summary>
        /// Get Tests By Params and Support Pagination
        /// </summary>
        /// <param name="specParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Test>>> GetTests(
            [FromQuery] TestSpecParams specParams)
        {
            var testDtos = await _mediator.Send(new GetTestsQuery(specParams));

            return PaginationOkResult<TestResponseDTO>(
                testDtos.Data,
                testDtos.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
        }
    }
}
