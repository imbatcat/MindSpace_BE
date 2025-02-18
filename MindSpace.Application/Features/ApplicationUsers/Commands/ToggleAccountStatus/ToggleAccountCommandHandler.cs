using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Interfaces.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.ToggleAccountStatus
{
    public class ToggleAccountStatusCommandHandler : IRequestHandler<ToggleAccountStatusCommand>
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly ILogger<ToggleAccountStatusCommandHandler> _logger;
        public ToggleAccountStatusCommandHandler(IApplicationUserService applicationUserService, ILogger<ToggleAccountStatusCommandHandler> logger)
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