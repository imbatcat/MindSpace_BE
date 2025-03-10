using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentications.Commands.RefreshUserAccessToken
{
    internal class RefreshUserAccessTokenCommandHandler
        (ILogger<RefreshUserAccessTokenCommandHandler> logger,
        IUserTokenService userTokenService,
        UserManager<ApplicationUser> userManager) : IRequestHandler<RefreshUserAccessTokenCommand, RefreshUserAccessTokenDTO>
    {
        public async Task<RefreshUserAccessTokenDTO> Handle(RefreshUserAccessTokenCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new access token for user {username}", request.User.UserName);
            var roles = await userManager.GetRolesAsync(request.User);

            var accessToken = userTokenService.CreateAccessToken(request.User, roles.First());
            var refreshToken = userTokenService.CreateRefreshToken(request.User);

            request.User.RefreshToken = refreshToken;
            await userManager.UpdateAsync(request.User);

            return new RefreshUserAccessTokenDTO()
            {
                AccesToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
    }
}