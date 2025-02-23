using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms
{
    public class GetSupportingProgramQueryHandler : IRequestHandler<GetSupportingProgramsQuery, PagedResultDTO<SupportingProgramResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetSupportingProgramQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public GetSupportingProgramQueryHandler(
            ILogger<GetSupportingProgramQueryHandler> logger,
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

        public async Task<PagedResultDTO<SupportingProgramResponseDTO>> Handle(GetSupportingProgramsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Supporting Programs with Spec: {@Spec}", request.SpecParams);

            var spec = new SupportingProgramSpecification(request.SpecParams);

            // Use Projection
            var listDto = await _unitOfWork
                .Repository<SupportingProgram>()
                .GetAllWithSpecProjectedAsync<SupportingProgramResponseDTO>(spec, _mapper.ConfigurationProvider);

            var count = await _unitOfWork
                .Repository<SupportingProgram>()
                .CountAsync(spec);

            return new PagedResultDTO<SupportingProgramResponseDTO>(count, listDto);
        }
    }
}