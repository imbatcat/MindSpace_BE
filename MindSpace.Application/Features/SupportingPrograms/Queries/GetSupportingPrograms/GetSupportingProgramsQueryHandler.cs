using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.SupportingPrograms.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms
{
    public class GetSupportingProgramsQueryHandler : IRequestHandler<GetSupportingProgramsQuery, IReadOnlyList<SupportingProgram>>
    {
        private readonly ILogger<GetSupportingProgramsQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetSupportingProgramsQueryHandler(
            ILogger<GetSupportingProgramsQueryHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IReadOnlyList<SupportingProgram>> Handle(GetSupportingProgramsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Supporting Programs with Spec: {@Spec}", request.SpecParams);
            var spec = new SupportingProgramSpecification(request.SpecParams);
            var items = await _unitOfWork.Repository<SupportingProgram>().GetAllWithSpecAsync(spec);
            return items;
        }
    }
}
