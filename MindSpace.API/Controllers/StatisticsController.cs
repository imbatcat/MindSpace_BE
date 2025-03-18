using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics.TestQuestionResponseStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTestQuestionResponseStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTestResponseRankAnalysisStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTestResponseTimeAnalysisStatistics;

namespace MindSpace.API.Controllers
{
    public class StatisticsController
        (IMediator mediator) : BaseApiController
    {
        // test responses statistics
        [HttpGet("test-responses/time-analysis")]
        public async Task<ActionResult<TimeGroupAnalysisDto>> GetTestResponseTimeAnalysisStatistics(
        [FromQuery] GetTestResponseTimeAnalysisStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return result;
        }

        [HttpGet("test-responses/score-rank-analysis")]
        public async Task<ActionResult<RankGroupAnalysisDto>> GetTestResponseRankAnalysisStatistics(
        [FromQuery] GetTestResponseRankAnalysisStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return result;
        }

        [HttpGet("test-responses/question-responses-analysis")]
        public async Task<ActionResult<TestQuestionResponseStatisticsAnalysisDto>> GetTestQuestionResponseStatistics(
        [FromQuery] GetTestQuestionResponseStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return result;
        }


        // appointment history statistics
        

        // supporting program statistics
    }
}
