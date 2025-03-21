using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.Tests.Queries.GetTestById;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Tests.Queries.GetMostRecentTests
{
    public class GetMostRecentTestsQueryHandler : IRequestHandler<GetMostRecentTestsQuery, List<TestOverviewResponseDTO>>
    {
        // === Fields & Props
        // ================================

        private readonly ILogger<GetTestByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================
        public GetMostRecentTestsQueryHandler(
            ILogger<GetTestByIdQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // ================================
        // === Methods
        // ================================
        public async Task<List<TestOverviewResponseDTO>> Handle(GetMostRecentTestsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Most Recent Test");

            var spec = new TestSpecification(request.SchoolId, request.Top, request.StartDate, request.EndDate);

            var data = await _unitOfWork
                .Repository<Test>()
                .GetAllWithSpecAsync(spec);

            if (data == null)
            {
                throw new NotFoundException("No test created in last 30 days");
            }

            return _mapper.Map<List<TestOverviewResponseDTO>>(data);
        }
    }
}
