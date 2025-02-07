using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.SupportingProgramHistories.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.SupportingProgramHistories.Queries.GetCountSupportingProgramHistories
{
    public class GetCountSupportingProgramHistoryQueryHandler : IRequestHandler<GetCountSupportingProgramHistoryQuery, int>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetCountSupportingProgramHistoryQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        // ================================
        // === Constructors
        // ================================

        public GetCountSupportingProgramHistoryQueryHandler(
            ILogger<GetCountSupportingProgramHistoryQueryHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<int> Handle(GetCountSupportingProgramHistoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get count of Supporting Program History with Spec: {@Spec}", request.SpecParams);
            var spec = new SupportingProgramHistorySpecification(request.SpecParams);
            var count = await _unitOfWork.Repository<SupportingProgramHistory>().CountAsync(spec);
            return count;
        }
    }
}
