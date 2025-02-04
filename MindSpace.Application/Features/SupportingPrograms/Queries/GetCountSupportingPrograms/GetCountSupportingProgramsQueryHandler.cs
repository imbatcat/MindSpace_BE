using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.SupportingPrograms.Specifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetCountSupportingPrograms
{
    public class GetCountSupportingProgramsQueryHandler : IRequestHandler<GetCountSupportingProgramsQuery, int>
    {
        private readonly ILogger<GetCountSupportingProgramsQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetCountSupportingProgramsQueryHandler(ILogger<GetCountSupportingProgramsQueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(GetCountSupportingProgramsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get count of Supporting Programs with Spec: {@Spec}", request.SpecParams);
            var spec = new SupportingProgramSpecification(request.SpecParams);
            var count = _unitOfWork.Repository<SupportingProgram>().CountAsync(spec);
            return count;
        }
    }
}
