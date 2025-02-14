using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.Authentication.Commands.RevokeUser;
using System.Security.Claims;

namespace MindSpace.Application.Features.Authentication.Commands.LogoutUser
{
    public class LogoutUserCommandHandler(
        ILogger<LogoutUserCommandHandler> logger,
        IMediator mediator
    ) : IRequestHandler<LogoutUserCommand>
    {
        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Logging out user");

            request.Response.Cookies.Delete("refreshToken");

            var userId = request.Response.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await mediator.Send(new RevokeUserCommand { UserId = userId });

            logger.LogInformation("User logged out successfully");
        }
    }
}