using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.ApplicationUsers.Commands.ToggleAccountStatus;
using MindSpace.Application.Features.Tests.Commands.CreateTestImport;
using MindSpace.Application.Features.Tests.Commands.CreateTestManual;
using MindSpace.Application.Features.Tests.Commands.DeleteTest;
using MindSpace.Application.Features.Tests.Commands.ToggleTestStatus;
using MindSpace.Application.Features.Tests.Commands.UpdateTest;
using MindSpace.Application.Features.Tests.Queries.GetMostRecentTests;
using MindSpace.Application.Features.Tests.Queries.GetTestById;
using MindSpace.Application.Features.Tests.Queries.GetTests;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.API.Controllers;

public class TestsController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/tests
    //[Cache(30000)]
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
    //[Cache(600)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TestResponseDTO>> GetTestById(int id)
    {
        var test = await mediator.Send(new GetTestByIdQuery(id));
        return Ok(test);
    }

    //[Cache(600)]
    [HttpGet("most-recent-test")]
    public async Task<ActionResult<TestResponseDTO>> GetMostRecentTest([FromQuery] GetMostRecentTestsQuery query)
    {
        var test = await mediator.Send(query);
        return Ok(test);
    }

    // ==============================
    // === POST, PUT, DELETE, PATCH
    // ==============================

    // POST /api/tests/import
    //[InvalidateCache("/api/tests|")]
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
    //[InvalidateCache("/api/tests|")]
    [HttpPost("manual")]
    public async Task<IActionResult> CreateTestManual([FromBody] CreateTestManualCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTestById), new { result.Id }, null);
    }

    // PUT /api/tests/toggle-status
    //[InvalidateCache("/api/tests|")]
    [HttpPut("{id}/toggle-status")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SchoolManager}")]
    public async Task<IActionResult> ToggleTestStatus([FromRoute] int id)
    {
        await mediator.Send(new ToggleTestStatusCommand { TestId = id });
        return Ok("The test status is toggled!");
    }
    // PATCH /tests/id
    [HttpPatch("{id:int}")]
    // only allowed for tests is not published and has no response yet
    public async Task<IActionResult> UpdateTest(int id, [FromBody] UpdateTestCommand command)
    {
        command.TestId = id;
        await mediator.Send(command);
        return Ok($"The test is updated!");
    }

    // DELETE /tests/id
    // only allowed for tests is not published and has no response yet
    //[InvalidateCache("/api/tests|")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTest(int id)
    {
        await mediator.Send(new DeleteTestCommand { TestId = id });
        return NoContent();
    }
}
