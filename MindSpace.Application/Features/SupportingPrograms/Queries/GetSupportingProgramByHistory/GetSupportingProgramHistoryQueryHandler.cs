using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.Specifications.SupportingProgramHistorySpecifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramByHistory
{
    public class GetSupportingProgramHistoryQueryHandler
        : IRequestHandler<GetSupportingProgramHistoryQuery, PagedResultDTO<SupportingProgramResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetSupportingProgramHistoryQuery> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================
        public GetSupportingProgramHistoryQueryHandler(
            ILogger<GetSupportingProgramHistoryQuery> logger,
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

        public async Task<PagedResultDTO<SupportingProgramResponseDTO>> Handle(GetSupportingProgramHistoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Supporting Programs with History Spec: {@spec}", request.SpecParams);

            var spec = new SupportingProgramHistorySpecification(request.SpecParams);

            // Use Projection 
            var listDto = await _unitOfWork
                .Repository<SupportingProgramHistory>()
                .GetAllWithSpecProjectedAsync<SupportingProgramResponseDTO>(spec, _mapper.ConfigurationProvider);

            var count = await _unitOfWork
                .Repository<SupportingProgramHistory>()
                .CountAsync(spec);

            return new PagedResultDTO<SupportingProgramResponseDTO>(count, listDto);
        }
    }
}
