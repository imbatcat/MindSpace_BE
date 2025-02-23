using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestResponseSpecifications;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.TestResponses.Queries.GetTestResponseById
{
    public class GetTestResponseByIdQueryHandler : IRequestHandler<GetTestResponseByIdQuery, TestResponseResponseDTO>
    {
        // props and fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTestResponseByIdQueryHandler> _logger;

        // constructor
        public GetTestResponseByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetTestResponseByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        // methods
        public async Task<TestResponseResponseDTO?> Handle(GetTestResponseByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository<TestResponse>()
                .GetBySpecProjectedAsync<TestResponseResponseDTO>(
                    new TestResponseSpecification(request.Id),
                    _mapper.ConfigurationProvider);

            if (result == null)
            {
                throw new NotFoundException(nameof(TestResponse), request.Id.ToString());
            }

            return result;
        }
    }
}
