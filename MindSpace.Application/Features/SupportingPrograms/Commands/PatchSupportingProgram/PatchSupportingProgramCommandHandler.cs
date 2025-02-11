using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.SupportingPrograms.Commands.CreateSupportingProgram;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.PatchSupportingProgram
{
    public class PatchSupportingProgramCommandHandler : IRequestHandler<PatchSupportingProgramCommand>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<PatchSupportingProgramCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public PatchSupportingProgramCommandHandler(ILogger<PatchSupportingProgramCommandHandler> logger,
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

        public async Task Handle(PatchSupportingProgramCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Supporting Program with Id: {@id}", request.Id);

            var spec = new SupportingProgramSpecification(request.Id);
            var existingSP = await _unitOfWork.Repository<SupportingProgram>().GetBySpecAsync(spec);

            // If not existed then throw exception
            if (existingSP == null) throw new NotFoundException(nameof(SupportingProgram), request.Id.ToString());

            // Using Automapper to map only nonnull request
            _mapper.Map<PatchSupportingProgramCommand, SupportingProgram>(request, existingSP);

            // Update Resource
            var updatedSP = _unitOfWork.Repository<SupportingProgram>().Update(existingSP)
                ?? throw new UpdateFailedException(existingSP.Id.ToString());

            await _unitOfWork.CompleteAsync();
        }
    }
}
