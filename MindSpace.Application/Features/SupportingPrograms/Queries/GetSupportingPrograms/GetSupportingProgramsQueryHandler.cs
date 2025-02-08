using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.Features.SupportingPrograms.Specifications;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms
{
    public class GetSupportingProgramQueryHandler : IRequestHandler<GetSupportingProgramsQuery, PagedResultDTO<SupportingProgramDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================s

        private readonly ILogger<GetSupportingProgramQueryHandler> _logger;
        private readonly ISupportingProgramService _supportingProgramService;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public GetSupportingProgramQueryHandler(
            ILogger<GetSupportingProgramQueryHandler> logger,
            ISupportingProgramService supportingProgramService,
            IMapper mapper)
        {
            _supportingProgramService = supportingProgramService;
            _logger = logger;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<PagedResultDTO<SupportingProgramDTO>> Handle(GetSupportingProgramsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Supporting Programs with Spec: {@Spec}", request.SpecParams);
            var spec = new SupportingProgramSpecification(request.SpecParams);
            var items = await _supportingProgramService.GetAllSupportingProgramAsync(spec);
            var itemsDto = _mapper.Map<IReadOnlyList<SupportingProgramDTO>>(items);
            var count = await _supportingProgramService.CountSupportingProgramAsync(spec);
            return new PagedResultDTO<SupportingProgramDTO>(count, itemsDto);
        }
    }
}
