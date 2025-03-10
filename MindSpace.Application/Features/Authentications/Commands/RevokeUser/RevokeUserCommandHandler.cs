using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentications.Commands.RevokeUser
{
    internal class RevokeUserCommandHandler(
        ILogger<RevokeUserCommandHandler> logger,
        IUserTokenService userTokenService,
        UserManager<ApplicationUser> userManager) : IRequestHandler<RevokeUserCommand>
    {
        public async Task Handle(RevokeUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Revoking refresh token for user {userId}", request.UserId);

            await userTokenService.RevokeUserToken(request.UserId);

            logger.LogInformation("Successfully revoked refresh token for user {userId}", request.UserId);
        }
    }
}