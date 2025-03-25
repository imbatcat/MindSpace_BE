using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Tests.Queries.GetTestById
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, TestResponseDTO>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetTestByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================
        public GetTestByIdQueryHandler(
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
        public async Task<TestResponseDTO> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Test By Id: {@Id}", request.Id);

            var spec = new TestSpecification(request.Id);

            var dataDto = await _unitOfWork
                .Repository<Test>()
                .GetBySpecProjectedAsync<TestResponseDTO>(spec, _mapper.ConfigurationProvider);

            if (dataDto == null)
            {
                throw new NotFoundException(nameof(Test), request.Id.ToString());
            }

            return dataDto;
        }
    }
}
