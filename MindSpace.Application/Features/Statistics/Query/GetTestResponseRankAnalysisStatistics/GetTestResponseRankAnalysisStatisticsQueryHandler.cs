using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestResponseSpecifications;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Statistics.Query.GetTestResponseRankAnalysisStatistics
{
    public class GetTestResponseRankAnalysisStatisticsQueryHandler(
            ILogger<GetTestResponseRankAnalysisStatisticsQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper) : IRequestHandler<GetTestResponseRankAnalysisStatisticsQuery, RankGroupAnalysisDto>
    {
        public async Task<RankGroupAnalysisDto> Handle(GetTestResponseRankAnalysisStatisticsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get statistics of Test Responses by time group analysis with Spec: {@Spec}", request);
            var specification = new TestResponseSpecification(request.TestId, request.StartDate, request.EndDate, request.SchoolId, false);

            // Get the filtered list of TestResponse objects
            var testResponses = await unitOfWork.Repository<TestResponse>().GetAllWithSpecAsync(specification);

            int totalTestResponsesCount = testResponses.Count;

            IEnumerable<IGrouping<string, TestResponse>> groupedData = testResponses.GroupBy(tr => tr.TestScoreRankResult ?? throw new NotFoundException("This test does not contain score rank criteria!"));
            
            var rankGroups = new List<RankGroupDto>();
            foreach (var group in groupedData)
            {
                var rankGroup = RankGroupDto.MapToRankGroupDto(group);
                rankGroup.Percentage = Math.Round(((double)rankGroup.ResponseCount / (double)totalTestResponsesCount) * 100,2);
                rankGroups.Add(rankGroup);
            }

            var result = new RankGroupAnalysisDto
            {
                TestId = request.TestId,
                SchoolId = request.SchoolId,
                TotalResponses = totalTestResponsesCount,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                RankGroups = rankGroups.OrderByDescending(r => r.AverageScore).ToList()
            };

            return result;
        }
    }
}
