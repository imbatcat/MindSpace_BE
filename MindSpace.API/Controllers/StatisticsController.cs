using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Statistics;
using MindSpace.Application.DTOs.Statistics.AppointmentStatistics;
using MindSpace.Application.DTOs.Statistics.SupportingProgramStatistics;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics.TestQuestionResponseStatistics;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Statistics.Query.GetAppointmentGroupBySpecialization;
using MindSpace.Application.Features.Statistics.Query.GetOverviewStatistics;
using MindSpace.Application.Features.Statistics.Query.GetSupportingProgramGroupBySpecialization;
using MindSpace.Application.Features.Statistics.Query.GetTestQuestionResponseStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTestResponseRankAnalysisStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTestResponseTimeAnalysisStatistics;
using MindSpace.Application.Features.Tests.Queries.GetMostRecentTests;

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

        [HttpGet("top-recent-tests")]
        public async Task<ActionResult<List<TestOverviewResponseDTO>>> GetTopRecentTests(
        [FromQuery] GetMostRecentTestsQuery query)
        {
            var result = await mediator.Send(query);
            return result;
        }

        // appointment history statistics
        [HttpGet("appointments")]
        public async Task<ActionResult<AppointmentGroupBySpecializationDTO>> GetAppointmentCountBySpecialization(
        [FromQuery] GetAppointmentGroupBySpecializationQuery query)
        {
            var result = await mediator.Send(query);
            return result;
        }

        // supporting program statistics
        [HttpGet("supporting-programs")]
        public async Task<ActionResult<SupportingProgramsGroupBySpecializationDTO>> GetSupportingProgramCountBySpecialization(
        [FromQuery] GetSupportingProgramGroupBySpecializationQuery query)
        {
            var result = await mediator.Send(query);
            return result;
        }

        [HttpGet("count-overview-data")]
        public async Task<ActionResult<CountOverviewDTO>> GetOverviewStatistics(
        [FromQuery] GetOverviewStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return result;
        }

    }
}
