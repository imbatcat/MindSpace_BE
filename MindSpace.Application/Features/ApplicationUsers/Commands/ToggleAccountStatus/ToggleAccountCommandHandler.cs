using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.Authentication;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.ToggleAccountStatus
{
    public class ToggleAccountStatusCommandHandler : IRequestHandler<ToggleAccountStatusCommand>
    {
        private readonly IApplicationUserRepository _applicationUserService;
        private readonly ILogger<ToggleAccountStatusCommandHandler> _logger;
        public ToggleAccountStatusCommandHandler(IApplicationUserRepository applicationUserService, ILogger<ToggleAccountStatusCommandHandler> logger)
        {
            _applicationUserService = applicationUserService;
            _logger = logger;
        }

        public async Task Handle(ToggleAccountStatusCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Toggling account status for user {UserId}", request.UserId);
            await _applicationUserService.ToggleAccountStatusAsync(request.UserId);
        }
    }
}