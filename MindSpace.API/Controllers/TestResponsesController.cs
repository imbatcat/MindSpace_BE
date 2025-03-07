using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.TestResponses.Commands.CreateTestResponse;
using MindSpace.Application.Features.TestResponses.Queries.GetTestResponseById;
using MindSpace.Application.Features.TestResponses.Queries.GetTestResponses;
using MindSpace.Application.Specifications.TestResponseSpecifications;

namespace MindSpace.API.Controllers;

[Route("api/v{version:apiVersion}/test-responses")]
public class TestResponsesController(IMediator mediator) : BaseApiController
{
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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TestResponseResponseDTO>> GetTestResponseById(int id)
    {
        var testResponse = await mediator.Send(new GetTestResponseByIdQuery(id));
        return Ok(testResponse);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTestResponse([FromBody] CreateTestResponseCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTestResponseById), new { result.Id }, null);
    }
}
