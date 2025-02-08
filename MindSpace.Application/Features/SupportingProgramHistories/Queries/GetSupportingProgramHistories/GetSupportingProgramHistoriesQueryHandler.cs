using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.SupportingProgramHistories.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.SupportingProgramHistories.Queries.GetSupportingProgramHistories
{
    public class GetSupportingProgramHistoriesQueryHandler : IRequestHandler<GetSupportingProgramHistoriesQuery, IReadOnlyList<SupportingProgramHistory>>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetSupportingProgramHistoriesQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        // ================================
        // === Constructors
        // ================================

        public GetSupportingProgramHistoriesQueryHandler(
            ILogger<GetSupportingProgramHistoriesQueryHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<IReadOnlyList<SupportingProgramHistory>> Handle(GetSupportingProgramHistoriesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Supporting Programs History with Spec: {@Spec}", request.SpecParams);
            var spec = new SupportingProgramHistorySpecification(request.SpecParams);
            var items = await _unitOfWork.Repository<SupportingProgramHistory>().GetAllWithSpecAsync(spec);
            return items;
        }
    }
}
