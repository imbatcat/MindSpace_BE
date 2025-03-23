using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.ToggleAccountStatus;

public class ToggleAccountStatusCommandHandler(
    IApplicationUserService<ApplicationUser> applicationUserService,
    ILogger<ToggleAccountStatusCommandHandler> logger) : IRequestHandler<ToggleAccountStatusCommand>
{
    public async Task Handle(ToggleAccountStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Toggling account status for user {UserId}", request.UserId);
        await applicationUserService.ToggleAccountStatusAsync(request.UserId);
    }
}