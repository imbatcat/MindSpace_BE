using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Tests.Commands.CreateTestImport;
using MindSpace.Application.Features.Tests.Commands.CreateTestManual;
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
        public async Task<ActionResult<IReadOnlyList<TestOverviewResponseDTO>>> GetTests(
            [FromQuery] TestSpecParams specParams)
        {
            var testDtos = await _mediator.Send(new GetTestsQuery(specParams));

            return PaginationOkResult<TestOverviewResponseDTO>(
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

        // ====================================
        // === CREATE, PATCH, DELETE
        // ====================================

        [HttpPost("import")]
        public async Task<IActionResult> CreateTestWithImport([FromForm] CreateTestImportCommand command)
        {
            if (command.TestFile == null || command.TestFile.Length == 0)
            {
                return BadRequest("File Excel cannot be empty");
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTestById), new { result.Id }, null);
        }

        [HttpPost("manual")]
        public async Task<IActionResult> CreateTestManual([FromBody] CreateTestManualCommand command)
        {
            // luu test draft tu redis vao db
            // luu y: generate test code bang cach hash tu test name
            var result = await _mediator.Send(command);
            return Ok(); // replace later
        }
    }
}
