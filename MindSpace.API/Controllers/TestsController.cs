using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Tests.Commands.CreateTestImport;
using MindSpace.Application.Features.Tests.Commands.CreateTestManual;
using MindSpace.Application.Features.Tests.Queries.GetTestById;
using MindSpace.Application.Features.Tests.Queries.GetTests;
using MindSpace.Application.Specifications.TestSpecifications;

namespace MindSpace.API.Controllers;

public class TestsController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/tests
    [Cache(30000)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TestOverviewResponseDTO>>> GetTests(
        [FromQuery] TestSpecParams specParams)
    {
        var testDtos = await mediator.Send(new GetTestsQuery(specParams));

        return PaginationOkResult<TestOverviewResponseDTO>(
            testDtos.Data,
            testDtos.Count,
            specParams.PageIndex,
            specParams.PageSize
        );
    }

    // GET /api/tests/{id}
    [Cache(600)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TestResponseDTO>> GetTestById(int id)
    {
        var test = await mediator.Send(new GetTestByIdQuery(id));
        return Ok(test);
    }

    // ==============================
    // === POST, PUT, DELETE, PATCH
    // ==============================

    // POST /api/tests/import
    [InvalidateCache("/api/tests|")]
    [HttpPost("import")]
    public async Task<IActionResult> CreateTestWithImport([FromForm] CreateTestImportCommand command)
    {
        if (command.TestFile == null || command.TestFile.Length == 0)
        {
            return BadRequest("File Excel cannot be empty");
        }

        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTestById), new { result.Id }, null);
    }

    // POST /api/tests/manual
    [InvalidateCache("/api/tests|")]
    [HttpPost("manual")]
    public async Task<IActionResult> CreateTestManual([FromBody] CreateTestManualCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTestById), new { result.Id }, null);
    }
}
