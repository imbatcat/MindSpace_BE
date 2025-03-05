using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.QuestionSpecifications;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.Questions.Queries.GetQuestions
{
    public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, PagedResultDTO<QuestionResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetQuestionsQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public GetQuestionsQueryHandler(
            ILogger<GetQuestionsQueryHandler> logger,
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

        public async Task<PagedResultDTO<QuestionResponseDTO>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Questions with Spec: {@Spec}", request.SpecParams);

            var spec = new QuestionSpecification(request.SpecParams);

            // Use Projection map to DTO
            var listDto = await _unitOfWork.Repository<Question>().GetAllWithSpecProjectedAsync<QuestionResponseDTO>(spec, _mapper.ConfigurationProvider);


            var count = await _unitOfWork
                 .Repository<Question>()
                 .CountAsync(spec);

            return new PagedResultDTO<QuestionResponseDTO>(count, listDto);
        }
    }
}