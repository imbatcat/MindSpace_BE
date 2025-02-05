using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.Authentication.Commands.RegisterUser;
using MindSpace.Application.Features.Authentication.DTOs;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using Restaurants.Applications.Ultilities.Identity.Authentication;

namespace MindSpace.Application.Features.Authentication.Commands.LoginUser
{
    internal class LoginUserCommandHandler
        (ILogger<RegisterUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IdTokenProvider idTokenProvider,
        AccessTokenProvider accessTokenProvider) : IRequestHandler<LoginUserCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            //TODO: Add !user.EmailVerified)
            if (user is null)
            {
                throw new BadHttpRequestException($"User with email: {request.Email} was not found");
            }

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, request.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new BadHttpRequestException("Incorrect password");
            }

            //TODO: maybe add 'remember me' option
            var result = await signInManager.PasswordSignInAsync(
                user,
                request.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded) //result may not succeed due to invalid 2FA code, not just incorrect password.
            {
                throw new UnauthorizedAccessException("Your authentication attempt failed, please try again with valid credentials");
            }

            var userRoles = await userManager.GetRolesAsync(user);

            string accessToken = accessTokenProvider.CreateToken(user, userRoles.First()!);
            string idToken = idTokenProvider.CreateToken(user);

            return new LoginResponse
            {
                AccessToken = accessToken,
                IdToken = idToken
            };
        }
    }
}