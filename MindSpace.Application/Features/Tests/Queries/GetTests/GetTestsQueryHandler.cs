using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestResponseSpecifications;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.Tests.Queries.GetTests
{
    public class GetTestsQueryHandler : IRequestHandler<GetTestsQuery, PagedResultDTO<TestOverviewResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetTestsQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public GetTestsQueryHandler(
            ILogger<GetTestsQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<PagedResultDTO<TestOverviewResponseDTO>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Tests with Spec: {@Spec}", request.SpecParams);

            // Get completed test IDs for the student if StudentId is provided
            // Only consider tests completed today
            var completedTestIds = new List<int>();
            if (request.SpecParams.StudentId.HasValue)
            {
                // Get range from 00:00 to 23:59 of current day
                var now = DateTime.Now;
                var startOfDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                var endOfDay = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

                var testResponseSpec = new TestResponseSpecification(
                    request.SpecParams.StudentId.Value,
                    startOfDay,
                    endOfDay
                );

                var completedTests = await _unitOfWork.Repository<TestResponse>()
                    .GetAllWithSpecAsync(testResponseSpec);

                // Get the testid that has been completed by the student, per day
                completedTestIds = completedTests.Select(tr => tr.TestId).ToList();
            }

            // Get the test that are not in the completed list
            var spec = new TestSpecification(request.SpecParams, completedTestIds);

            // Use Projection map to DTO
            var listDto = await _unitOfWork.Repository<Test>().GetAllWithSpecProjectedAsync<TestOverviewResponseDTO>(spec, _mapper.ConfigurationProvider);

            var count = await _unitOfWork
                 .Repository<Test>()
                 .CountAsync(spec);

            return new PagedResultDTO<TestOverviewResponseDTO>(count, listDto);
        }
    }
}