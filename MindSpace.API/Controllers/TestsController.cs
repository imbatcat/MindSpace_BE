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
    // GET /api/v1/tests
    // Get all tests with pagination and filtering
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

    // GET /api/v1/tests/{id}
    // Get a specific test by ID
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TestResponseDTO>> GetTestById(int id)
    {
        var test = await mediator.Send(new GetTestByIdQuery(id));
        return Ok(test);
    }

    // GET /api/v1/tests/most-recent-test
    // Get the most recent test based on query parameters
    [HttpGet("most-recent-test")]
    public async Task<ActionResult<TestResponseDTO>> GetMostRecentTest([FromQuery] GetMostRecentTestsQuery query)
    {
        var test = await mediator.Send(query);
        return Ok(test);
    }

    // POST /api/v1/tests/import
    // Create a new test by importing from Excel file
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

    // POST /api/v1/tests/manual
    // Create a new test manually
    [HttpPost("manual")]
    public async Task<IActionResult> CreateTestManual([FromBody] CreateTestManualCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTestById), new { result.Id }, null);
    }

    // PUT /api/v1/tests/{id}/toggle-status
    // Toggle the status of a test (Admin and SchoolManager only)
    [HttpPut("{id}/toggle-status")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SchoolManager}")]
    public async Task<IActionResult> ToggleTestStatus([FromRoute] int id)
    {
        await mediator.Send(new ToggleTestStatusCommand { TestId = id });
        return Ok("The test status is toggled!");
    }

    // PATCH /api/v1/tests/{id}
    // Update a test (only allowed for unpublished tests with no responses)
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateTest(int id, [FromBody] UpdateTestCommand command)
    {
        command.TestId = id;
        await mediator.Send(command);
        return Ok($"The test is updated!");
    }

    // DELETE /api/v1/tests/{id}
    // Delete a test (only allowed for unpublished tests with no responses)
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTest(int id)
    {
        await mediator.Send(new DeleteTestCommand { TestId = id });
        return NoContent();
    }
}
