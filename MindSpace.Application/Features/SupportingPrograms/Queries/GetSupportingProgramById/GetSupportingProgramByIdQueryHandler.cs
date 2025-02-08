using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById
{
    public class GetSupportingProgramByIdQueryHandler : IRequestHandler<GetSupportingProgramByIdQuery, SupportingProgramDTO>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetSupportingProgramByIdQueryHandler> _logger;
        private readonly ISupportingProgramService _supportingProgramService;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================
        public GetSupportingProgramByIdQueryHandler(
            ILogger<GetSupportingProgramByIdQueryHandler> logger,
            ISupportingProgramService supportingProgramService,
            IMapper mapper)
        {
            _logger = logger;
            _supportingProgramService = supportingProgramService;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================
        public async Task<SupportingProgramDTO> Handle(GetSupportingProgramByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Supporting Program By Id: {@Id}", request.Id);
            var supportingProgram = await _supportingProgramService.GetSupportingProgramByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(SupportingProgram), request.Id.ToString());

            var supportingProgramDto = _mapper.Map<SupportingProgramDTO>(supportingProgram);

            return supportingProgramDto;
        }
    }
}
