using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById
{
    public class GetSupportingProgramByIdQueryHandler : IRequestHandler<GetSupportingProgramByIdQuery, SupportingProgramWithStudentsResponseDTO>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetSupportingProgramByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================
        public GetSupportingProgramByIdQueryHandler(
            ILogger<GetSupportingProgramByIdQueryHandler> logger,
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
        public async Task<SupportingProgramWithStudentsResponseDTO> Handle(GetSupportingProgramByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Supporting Program By Id: {@Id}", request.Id);

            var spec = new SupportingProgramSpecification(request.Id);

            var dataDto = await _unitOfWork
                .Repository<SupportingProgram>()
                .GetBySpecProjectedAsync<SupportingProgramWithStudentsResponseDTO>(spec, _mapper.ConfigurationProvider);

            if (dataDto == null)
            {
                throw new NotFoundException(nameof(SupportingProgram), request.Id.ToString());
            }

            return dataDto;
        }
    }
}