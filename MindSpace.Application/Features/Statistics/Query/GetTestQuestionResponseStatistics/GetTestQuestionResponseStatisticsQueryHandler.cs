using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Statistics.TestResponseStatistics.TestQuestionResponseStatistics;
using MindSpace.Application.Features.Statistics.Query.GetTestResponseRankAnalysisStatistics;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestResponseSpecifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.Statistics.Query.GetTestQuestionResponseStatistics
{
    public class GetTestQuestionResponseStatisticsQueryHandler(
            ILogger<GetTestResponseRankAnalysisStatisticsQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper) : IRequestHandler<GetTestQuestionResponseStatisticsQuery, TestQuestionResponseStatisticsAnalysisDto>
    {
        public async Task<TestQuestionResponseStatisticsAnalysisDto> Handle(GetTestQuestionResponseStatisticsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get statistics of Test Responses with Spec: {@Spec}", request);
            var specification = new TestResponseSpecification(request.TestId, request.StartDate, request.EndDate, request.SchoolId, true);

            var testResponses = await unitOfWork.Repository<TestResponse>().GetAllWithSpecAsync(specification);

            // get flatten list of all test response items
            var testResponseItems = testResponses
                .SelectMany(tr => tr.TestResponseItems)
                .ToList();

            // Group test response items by QuestionContent and AnswerText
            var groupedByQuestion = GroupByQuestionAndAnswer(testResponseItems);

            // Transform into TestQuestionResponseStatisticsDto
            var testQuestionResponseStatistics = TransformToStatisticsDtos(groupedByQuestion);

            var result = new TestQuestionResponseStatisticsAnalysisDto
            {
                TestId = request.TestId,
                SchoolId = request.SchoolId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TestQuestionResponseStatistics = testQuestionResponseStatistics
            };

            return result;
        }

        private List<(string QuestionContent, List<(string AnswerOption, int Count)> Answers, int TotalResponses)> GroupByQuestionAndAnswer(List<TestResponseItem> items)
        {
            return items
                .GroupBy(item => item.QuestionContent)
                .Select(qGroup => (
                    QuestionContent: qGroup.Key,
                    Answers: qGroup
                        .GroupBy(item => item.AnswerText)
                        .Select(aGroup => (AnswerOption: aGroup.Key, Count: aGroup.Count()))
                        .ToList(),
                    TotalResponses: qGroup.Count()
                ))
                .ToList();
        }

        private List<TestQuestionResponseStatisticsDto> TransformToStatisticsDtos(List<(string QuestionContent, List<(string AnswerOption, int Count)> Answers, int TotalResponses)> groupedData)
        {
            return groupedData
                .Select(q => new TestQuestionResponseStatisticsDto
                {
                    QuestionContent = q.QuestionContent,
                    Slices = q.Answers
                        .Select(a => new TestQuestionResponseStatisticsSliceDto
                        {
                            AnswerOption = a.AnswerOption,
                            Count = a.Count,
                            Percentage = q.TotalResponses > 0 ? Math.Round((double)a.Count / q.TotalResponses * 100, 2) : 0
                        })
                        .ToList()
                })
                .OrderBy(q => q.QuestionContent)
                .ToList();
        }
    }
}
