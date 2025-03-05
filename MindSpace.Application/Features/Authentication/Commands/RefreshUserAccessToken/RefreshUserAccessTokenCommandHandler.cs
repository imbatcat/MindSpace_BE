using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentication.Commands.RefreshUserAccessToken
{
    internal class RefreshUserAccessTokenCommandHandler
        (ILogger<RefreshUserAccessTokenCommandHandler> logger,
        IAccessTokenProvider accessTokenProvider,
        IRefreshTokenProvider refreshTokenProvider,
        UserManager<ApplicationUser> userManager) : IRequestHandler<RefreshUserAccessTokenCommand, RefreshUserAccessTokenDTO>
    {
        public async Task<RefreshUserAccessTokenDTO> Handle(RefreshUserAccessTokenCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new access token for user {username}", request.User.UserName);
            var roles = await userManager.GetRolesAsync(request.User);

            var accessToken = accessTokenProvider.CreateToken(request.User, roles.First());
            var refreshToken = refreshTokenProvider.CreateToken(request.User);

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