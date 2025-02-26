using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.TestResponses.Queries.GetTestScoreRankByTotalScore;

namespace MindSpace.API.Controllers
{
    public class TestScoreRanksController : BaseApiController
    {
        // 1. get score ranks based on total score
        //==================
        // props and fields
        //==================
        private readonly IMediator _mediator;

        // ============================
        // Constructors
        // ============================
        public TestScoreRanksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ============================
        // GET
        // ============================
        [HttpGet]
        public async Task<ActionResult<TestScoreRankResponseDTO>> GetTestScoreRankByTotalScore([FromQuery] int totalScore, int testId)
        {
            var data = await _mediator.Send(new GetTestScoreRankByTotalScoreQuery(totalScore, testId));
            return Ok(data);
        }

    }
}
