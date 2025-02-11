using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentication.Commands.RevokeUser
{
    internal class RevokeUserCommandHandler(
        ILogger<RevokeUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager) : IRequestHandler<RevokeUserCommand>
    {
        public async Task Handle(RevokeUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Revoking refresh token for user {userId}", request.UserId);

            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.UserId} not found");
            }

            user.RefreshToken = null;
            await userManager.UpdateAsync(user);

            logger.LogInformation("Successfully revoked refresh token for user {userId}", request.UserId);
        }
    }
}