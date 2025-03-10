using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using System.Security.Claims;

namespace MindSpace.Application.Features.Authentications.Commands.LogoutUser
{
    public class LogoutUserCommandHandler(
        ILogger<LogoutUserCommandHandler> logger,
        IUserTokenService userTokenService
    ) : IRequestHandler<LogoutUserCommand>
    {
        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Logging out user");

            request.Response.Cookies.Delete("refreshToken");

            var userId = request.Response.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await userTokenService.RevokeUserToken(userId!);

            logger.LogInformation("User logged out successfully");
        }
    }
}