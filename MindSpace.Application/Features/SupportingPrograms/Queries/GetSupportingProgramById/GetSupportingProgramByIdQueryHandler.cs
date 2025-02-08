using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById
{
    public class GetSupportingProgramByIdQueryHandler : IRequestHandler<GetSupportingProgramByIdQuery, SupportingProgramDTO>
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
        public async Task<SupportingProgramDTO> Handle(GetSupportingProgramByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Supporting Program By Id: {@Id}", request.Id);
            return null;
        }
    }
}
