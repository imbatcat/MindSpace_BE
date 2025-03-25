using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestResponseSpecifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.TestResponses.Queries.GetTestResponses
{
    public class GetTestResponsesQueryHandler : IRequestHandler<GetTestResponsesQuery, PagedResultDTO<TestResponseOverviewResponseDTO>>
    {
        // props and fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTestResponsesQueryHandler> _logger;

        // constructor
        public GetTestResponsesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetTestResponsesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // methods
        public async Task<PagedResultDTO<TestResponseOverviewResponseDTO>> Handle(GetTestResponsesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Test Responses with Spec: {@Spec}", request.SpecParams);

            var spec = new TestResponseSpecification(request.SpecParams);

            // Use Projection map to DTO
            var listDto = await _unitOfWork.Repository<TestResponse>().GetAllWithSpecProjectedAsync<TestResponseOverviewResponseDTO>(spec, _mapper.ConfigurationProvider);

            var count = await _unitOfWork
                 .Repository<TestResponse>()
                 .CountAsync(spec);

            return new PagedResultDTO<TestResponseOverviewResponseDTO>(count, listDto);
        }
    }
}
