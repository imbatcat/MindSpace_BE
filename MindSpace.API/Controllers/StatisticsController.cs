using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.DTOs.Statistics;
using MindSpace.Application.DTOs.Statistics.AppointmentStatistics;
using MindSpace.Application.DTOs.Statistics.SupportingProgramStatistics;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics.TestQuestionResponseStatistics;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Statistics.Query.GetAppointmentGroupBySpecialization;
using MindSpace.Application.Features.Statistics.Query.GetOverviewStatistics;
using MindSpace.Application.Features.Statistics.Query.GetOverviewSystemStatistics;
using MindSpace.Application.Features.Statistics.Query.GetRevenueBySpecializationStatistics;
using MindSpace.Application.Features.Statistics.Query.GetRevenueTimeAnalysisStatistics;
using MindSpace.Application.Features.Statistics.Query.GetSupportingProgramGroupBySpecialization;
using MindSpace.Application.Features.Statistics.Query.GetTestQuestionResponseStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTestResponseRankAnalysisStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTestResponseTimeAnalysisStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTopPsychologistRevenueStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTopSchoolRevenue;
using MindSpace.Application.Features.Tests.Queries.GetMostRecentTests;

namespace MindSpace.API.Controllers
{
    public class StatisticsController
        (IMediator mediator) : BaseApiController
    {
        // ====================================
        // === GET
        // ====================================

        // GET /api/v1/statistics/test-responses/time-analysis
        // Get time-based analysis statistics for test responses
        [HttpGet("test-responses/time-analysis")]
        public async Task<ActionResult<TimeGroupAnalysisDto>> GetTestResponseTimeAnalysisStatistics(
            [FromQuery] GetTestResponseTimeAnalysisStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // GET /api/v1/statistics/test-responses/score-rank-analysis
        // Get rank-based analysis statistics for test responses
        [HttpGet("test-responses/score-rank-analysis")]
        public async Task<ActionResult<RankGroupAnalysisDto>> GetTestResponseRankAnalysisStatistics(
            [FromQuery] GetTestResponseRankAnalysisStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // GET /api/v1/statistics/test-responses/question-responses-analysis
        // Get analysis of question responses in tests
        [HttpGet("test-responses/question-responses-analysis")]
        public async Task<ActionResult<TestQuestionResponseStatisticsAnalysisDto>> GetTestQuestionResponseStatistics(
            [FromQuery] GetTestQuestionResponseStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // GET /api/v1/statistics/top-recent-tests
        // Get list of most recent tests
        [HttpGet("top-recent-tests")]
        public async Task<ActionResult<List<TestOverviewResponseDTO>>> GetTopRecentTests(
            [FromQuery] GetMostRecentTestsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // GET /api/v1/statistics/appointments
        // Get appointment statistics grouped by specialization
        [HttpGet("appointments")]
        public async Task<ActionResult<AppointmentGroupBySpecializationDTO>> GetAppointmentCountBySpecialization(
            [FromQuery] GetAppointmentGroupBySpecializationQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // GET /api/v1/statistics/supporting-programs
        // Get supporting program statistics grouped by specialization
        [HttpGet("supporting-programs")]
        public async Task<ActionResult<SupportingProgramsGroupBySpecializationDTO>> GetSupportingProgramCountBySpecialization(
            [FromQuery] GetSupportingProgramGroupBySpecializationQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("count-overview-school-data")]
        // GET /api/v1/statistics/count-overview-school-data
        // Get overview statistics with counts
        public async Task<ActionResult<CountSchoolOverviewDataDTO>> GetOverviewStatistics(
            [FromQuery] GetOverviewStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("overview-system-data")]
        public async Task<ActionResult<OverviewStatisticsDTO>> GetOverviewSystemStatistics(
            [FromQuery] GetOverviewSystemStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("revenue-time-analysis")]
        public async Task<ActionResult<List<RevenueStatDTO>>> GetRevenueStatistics(
            [FromQuery] GetRevenueTimeAnalysisStatisticsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("top-psychologists")]
        public async Task<ActionResult<List<PsychologistRevenueStatDTO>>> GetTopPsychologistRevenue(
            [FromQuery] GetTopPsychologistRevenueQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("revenue-spec-analysis")]
        public async Task<ActionResult<List<SpecializationRevenueStatDTO>>> GetRevenueBySpecialization(
            [FromQuery] GetRevenueBySpecializationQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("top-schools")]
        public async Task<ActionResult<List<SchoolRevenueDTO>>> GetTopSchoolRevenue(
            [FromQuery] GetTopSchoolRevenueQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
