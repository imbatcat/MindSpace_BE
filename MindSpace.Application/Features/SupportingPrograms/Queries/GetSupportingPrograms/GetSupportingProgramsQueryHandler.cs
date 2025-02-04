using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.SupportingPrograms.Specs;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var spec = new SupportingProgramSpecification(request.specParams);
            var items = await _unitOfWork.Repository<SupportingProgram>().GetAllWithSpecAsync(spec);
            return items;
        }
    }
}
