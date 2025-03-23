using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestResponseSpecifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.Statistics.Query.GetTestResponseTimeAnalysisStatistics
{
    public class GetTestResponseTimeAnalysisStatisticsQueryHandler(
            ILogger<GetTestResponseTimeAnalysisStatisticsQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper) : IRequestHandler<GetTestResponseTimeAnalysisStatisticsQuery, TimeGroupAnalysisDto>
    {
        public async Task<TimeGroupAnalysisDto> Handle(GetTestResponseTimeAnalysisStatisticsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get statistics of Test Responses by time group analysis with Spec: {@Spec}", request);
            var specification = new TestResponseSpecification(request.TestId, request.StartDate, request.EndDate, request.SchoolId, false);

            // Get the filtered list of TestResponse objects
            var testResponses = await unitOfWork.Repository<TestResponse>().GetAllWithSpecAsync(specification);

            // Group the data in-memory based on TimePeriod
            IEnumerable<IGrouping<string, TestResponse>> groupedData;
            switch (request.TimePeriod.ToLower())
            {
                case "day":
                    groupedData = testResponses.GroupBy(x => x.CreateAt.ToString("yyyy-MM-dd")); break;
                case "month":
                    groupedData = testResponses.GroupBy(x => x.CreateAt.ToString("yyyy-MM")); break;
                case "year":
                    groupedData = testResponses.GroupBy(x => x.CreateAt.ToString("yyyy")); break;
                default:
                    groupedData = testResponses.GroupBy(x => x.CreateAt.ToString("yyyy-MM-dd")); break;
            }

            // Transform the grouped data into TimeGroupDto objects
            var timeGroups = new List<TimeGroupDto>();
            foreach (var group in groupedData)
            {
                timeGroups.Add(TimeGroupDto.MapToTimeGroupDto(group, request.TimePeriod));
            }

            var result = new TimeGroupAnalysisDto
            {
                TestId = request.TestId,
                SchoolId = request.SchoolId,
                TimePeriod = request.TimePeriod,
                TimeGroups = timeGroups
            };

            return result;
        }
    }
}
