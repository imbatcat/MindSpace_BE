using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.TestResponses.Queries.GetTestScoreRankByTotalScore;

namespace MindSpace.API.Controllers;

[Route("api/v{version:apiVersion}/test-score-ranks")]
public class TestScoreRanksController(IMediator mediator) : BaseApiController
{
    // 1. get score ranks based on total score
    //==================
    // props and fields
    //==================

    // ============================
    // GET
    // ============================
    [HttpGet]
    public async Task<ActionResult<TestScoreRankResponseDTO>> GetTestScoreRankByTotalScore([FromQuery] int totalScore, int testId)
    {
        var data = await mediator.Send(new GetTestScoreRankByTotalScoreQuery(totalScore, testId));
        return Ok(data);
    }
}
