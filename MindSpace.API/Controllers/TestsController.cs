using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Tests.Queries.GetTestById;
using MindSpace.Application.Features.Tests.Queries.GetTests;
using MindSpace.Application.Specifications.TestSpecifications;

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
        public async Task<ActionResult<IReadOnlyList<TestResponseDTO>>> GetTests(
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

        /// <summary>
        /// Get Test By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TestResponseDTO>> GetTestById(int id)
        {
            var test = await _mediator.Send(new GetTestByIdQuery(id));
            return Ok(test);
        }
    }
}
