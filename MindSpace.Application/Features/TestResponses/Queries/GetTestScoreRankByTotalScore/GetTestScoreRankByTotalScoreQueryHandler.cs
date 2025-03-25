using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.TestScoreRankSpecifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.TestResponses.Queries.GetTestScoreRankByTotalScore
{
    public class GetTestScoreRankByTotalScoreQueryHandler : IRequestHandler<GetTestScoreRankByTotalScoreQuery, TestScoreRankResponseDTO>
    {
        // props and fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTestScoreRankByTotalScoreQueryHandler> _logger;

        // constructor
        public GetTestScoreRankByTotalScoreQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetTestScoreRankByTotalScoreQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // methods
        public async Task<TestScoreRankResponseDTO> Handle(GetTestScoreRankByTotalScoreQuery request, CancellationToken cancellationToken)
        {
            var testScoreRankDb = await _unitOfWork.Repository<TestScoreRank>()
                .GetBySpecAsync(new TestScoreRankSpecification(request.TestId, request.TotalScore));

            return _mapper.Map<TestScoreRankResponseDTO>(testScoreRankDb);
        }
    }
}
