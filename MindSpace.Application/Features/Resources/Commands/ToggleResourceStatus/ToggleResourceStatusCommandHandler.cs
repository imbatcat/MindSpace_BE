using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Resources.Commands.ToggleResourceStatus
{
    public class ToggleResourceStatusCommandHandler(
    IUnitOfWork _unitOfWork,
    ILogger<ToggleResourceStatusCommandHandler> _logger) : IRequestHandler<ToggleResourceStatusCommand>
    {
        public async Task Handle(ToggleResourceStatusCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            _logger.LogInformation("Toggling test status for resource {Id}", id);
            var resource = await _unitOfWork.Repository<Resource>().GetBySpecAsync(new ResourceSpecification(id));
            if (resource == null)
            {
                throw new NotFoundException(nameof(Resource), id.ToString());
            }

            resource.isActive = !resource.isActive;
            _unitOfWork.Repository<Resource>().Update(resource);
            await _unitOfWork.CompleteAsync();
        }
    }

}
