using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.SupportingPrograms.Queries.GetCountSupportingPrograms;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById
{
    public class GetSupportingProgramByIdQueryHandler : IRequestHandler<GetSupportingProgramByIdQuery, SupportingProgram>
    {
        private readonly ILogger<GetSupportingProgramByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetSupportingProgramByIdQueryHandler(ILogger<GetSupportingProgramByIdQueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<SupportingProgram> Handle(GetSupportingProgramByIdQuery request, CancellationToken cancellationToken)
        {
            var supportingProgram = await _unitOfWork.Repository<SupportingProgram>().GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(SupportingProgram), request.Id.ToString());

            return supportingProgram;
        }
    }
}
