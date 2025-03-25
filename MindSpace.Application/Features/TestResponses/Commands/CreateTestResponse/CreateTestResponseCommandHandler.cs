using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.TestResponses.Commands.CreateTestResponse
{
    public class CreateTestResponseCommandHandler : IRequestHandler<CreateTestResponseCommand, TestResponseOverviewResponseDTO>
    {
        // props and fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTestResponseCommandHandler> _logger;

        // constructor
        public CreateTestResponseCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateTestResponseCommandHandler> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // methods
        public async Task<TestResponseOverviewResponseDTO> Handle(CreateTestResponseCommand request, CancellationToken cancellationToken)
        {
            var testResponse = _mapper.Map<CreateTestResponseCommand, TestResponse>(request);

            _unitOfWork.Repository<TestResponse>().Insert(testResponse);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<TestResponse, TestResponseOverviewResponseDTO>(testResponse);
        }
    }
}
