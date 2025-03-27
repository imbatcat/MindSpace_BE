using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.Tests.Commands.ToggleTestStatus;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Entities.Tests;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.SupportingPrograms.Commands.ToggleSupportingProgramStatus
{
    public class ToggleSupportingProgramStatusCommandHandler(
    IUnitOfWork _unitOfWork,
    ILogger<ToggleSupportingProgramStatusCommandHandler> _logger) : IRequestHandler<ToggleSupportingProgramStatusCommand>
    {
        public async Task Handle(ToggleSupportingProgramStatusCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            _logger.LogInformation("Toggling status for supporting program {Id}", id);
            var supportingProgram = await _unitOfWork.Repository<SupportingProgram>().GetBySpecAsync(new SupportingProgramSpecification(id));
            if (supportingProgram == null)
            {
                throw new NotFoundException(nameof(SupportingProgram), id.ToString());
            }

            supportingProgram.IsActive = !supportingProgram.IsActive;
            _unitOfWork.Repository<SupportingProgram>().Update(supportingProgram);
            await _unitOfWork.CompleteAsync();
        }
    }
}
