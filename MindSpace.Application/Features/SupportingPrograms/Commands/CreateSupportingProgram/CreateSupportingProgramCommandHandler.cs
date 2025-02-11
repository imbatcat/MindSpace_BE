using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.CreateSupportingProgram
{
    public class CreateSupportingProgramCommandHandler
        : IRequestHandler<CreateSupportingProgramCommand, SupportingProgramResponseDTO>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<CreateSupportingProgramCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public CreateSupportingProgramCommandHandler(ILogger<CreateSupportingProgramCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<SupportingProgramResponseDTO> Handle(CreateSupportingProgramCommand request, CancellationToken cancellationToken)
        {
            //var spToCreate = _mapper.Map<SupportingProgram>(request);
            //SupportingProgram? addedSP = _unitOfWork.Repository<SupportingProgram>()
            //    .Update(spToCreate) ?? new NotFoundException(nameof(SupportingProgram), sp);

            //await _

            return null;
        }
    }
}
