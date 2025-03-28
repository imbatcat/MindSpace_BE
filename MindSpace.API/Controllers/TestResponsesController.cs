using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.TestResponses.Commands.CreateTestResponse;
using MindSpace.Application.Features.TestResponses.Queries.GetTestResponseById;
using MindSpace.Application.Features.TestResponses.Queries.GetTestResponses;
using MindSpace.Application.Specifications.TestResponseSpecifications;

namespace MindSpace.API.Controllers;

[Route("api/v{version:apiVersion}/test-responses")]
public class TestResponsesController(IMediator mediator) : BaseApiController
{
    // ====================================
    // === GET
    // ====================================

    // GET /api/v1/test-responses
    // Get all test responses with pagination and filtering
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TestResponseOverviewResponseDTO>>> GetTestResponses([FromQuery] TestResponseSpecParams specParams)
    {
        var data = await mediator.Send(new GetTestResponsesQuery(specParams));
        return PaginationOkResult<TestResponseOverviewResponseDTO>(
                data.Data,
                data.Count,
                specParams.PageIndex,
                specParams.PageSize
            );
    }

    // GET /api/v1/test-responses/{id}
    // Get a specific test response by ID
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TestResponseResponseDTO>> GetTestResponseById(int id)
    {
        var testResponse = await mediator.Send(new GetTestResponseByIdQuery(id));
        return Ok(testResponse);
    }

    // ==============================
    // === POST, PUT, DELETE, PATCH
    // ==============================

    // POST /api/v1/test-responses
    // Create a new test response
    [HttpPost]
    public async Task<ActionResult> CreateTestResponse([FromBody] CreateTestResponseCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTestResponseById), new { result.Id }, null);
    }
}
