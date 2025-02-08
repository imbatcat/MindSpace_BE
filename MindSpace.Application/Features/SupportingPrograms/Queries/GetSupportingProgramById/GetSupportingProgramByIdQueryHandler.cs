using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById
{
    public class GetSupportingProgramByIdQueryHandler : IRequestHandler<GetSupportingProgramByIdQuery, SupportingProgram>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetSupportingProgramByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        // ================================
        // === Constructors
        // ================================
        public GetSupportingProgramByIdQueryHandler(ILogger<GetSupportingProgramByIdQueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // ================================
        // === Methods
        // ================================
        public async Task<SupportingProgram> Handle(GetSupportingProgramByIdQuery request, CancellationToken cancellationToken)
        {
            var supportingProgram = await _unitOfWork.Repository<SupportingProgram>().GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(SupportingProgram), request.Id.ToString());

            return supportingProgram;
        }
    }
}
