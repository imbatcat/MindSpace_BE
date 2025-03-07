using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.ToggleAccountStatus;

public class ToggleAccountStatusCommandHandler(
    IApplicationUserRepository applicationUserService,
    ILogger<ToggleAccountStatusCommandHandler> logger) : IRequestHandler<ToggleAccountStatusCommand>
{
    public async Task Handle(ToggleAccountStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Toggling account status for user {UserId}", request.UserId);
        await applicationUserService.ToggleAccountStatusAsync(request.UserId);
    }
}