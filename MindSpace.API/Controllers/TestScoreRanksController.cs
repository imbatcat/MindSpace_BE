using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.TestResponses.Queries.GetTestScoreRankByTotalScore;

namespace MindSpace.API.Controllers;

[Route("api/v{version:apiVersion}/test-score-ranks")]
public class TestScoreRanksController(IMediator mediator) : BaseApiController
{
    // GET /api/test-score-ranks?totalScore={totalScore}&testId={testId}
    [HttpGet]
    public async Task<ActionResult<TestScoreRankResponseDTO>> GetTestScoreRankByTotalScore([FromQuery] int totalScore, int testId)
    {
        var data = await mediator.Send(new GetTestScoreRankByTotalScoreQuery(totalScore, testId));
        return Ok(data);
    }
}
