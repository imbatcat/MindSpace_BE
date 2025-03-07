using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Authentication.Commands.LoginUser
{
    internal class LoginUserCommandHandler
        (ILogger<LoginUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IIDTokenProvider idTokenProvider,
        IAccessTokenProvider accessTokenProvider,
        IRefreshTokenProvider refreshTokenProvider) : IRequestHandler<LoginUserCommand, LoginResponseDTO>
    {
        public async Task<LoginResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting login process for user with email: {Email}", request.Email);

            try
            {
                var user = await GetAndValidateUser(request.Email);
                ValidatePassword(user, request.Password);
                await SignInUser(user, request.Password);
                var userRole = await GetUserRole(user);
                var tokens = await GenerateTokens(user, userRole);

                logger.LogInformation("Login successful for user {UserId} with role {Role}", user.Id, userRole);

                return new LoginResponseDTO
                {
                    AccessToken = tokens.accessToken,
                    IdToken = tokens.idToken,
                    RefreshToken = tokens.refreshToken
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Login failed for email: {Email}", request.Email);
                throw;
            }
        }

        /// <summary>
        /// Retrieves and validates a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to find.</param>
        /// <returns>The found ApplicationUser if exists.</returns>
        /// <exception cref="BadHttpRequestException">Thrown when user is not found.</exception>
        private async Task<ApplicationUser> GetAndValidateUser(string email)
        {
            logger.LogDebug("Attempting to find user by email: {Email}", email);

            var user = await userManager.FindByEmailAsync(email);
            //TODO: Add check !user.EmailVerified)
            if (user is null)
            {
                logger.LogWarning("User not found with email: {Email}", email);
                throw new BadHttpRequestException($"User with email: {email} was not found");
            }

            logger.LogDebug("User found: {UserId}", user.Id);
            return user;
        }

        /// <summary>
        /// Validates the provided password against the user's stored password hash.
        /// </summary>
        /// <param name="user">The user whose password needs to be validated.</param>
        /// <param name="password">The password to validate.</param>
        /// <exception cref="BadHttpRequestException">Thrown when password validation fails.</exception>
        private void ValidatePassword(ApplicationUser user, string password)
        {
            logger.LogDebug("Validating password for user: {UserId}", user.Id);

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                logger.LogWarning("Password validation failed for user: {UserId}", user.Id);
                throw new BadHttpRequestException("Incorrect password");
            }

            logger.LogDebug("Password validation successful for user: {UserId}", user.Id);
        }

        /// <summary>
        /// Attempts to sign in the user using the provided credentials.
        /// </summary>
        /// <param name="user">The user attempting to sign in.</param>
        /// <param name="password">The password for authentication.</param>
        /// <returns>A task representing the asynchronous sign-in operation.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when sign-in fails.</exception>
        private async Task SignInUser(ApplicationUser user, string password)
        {
            logger.LogDebug("Attempting to sign in user: {UserId}", user.Id);

            //TODO: maybe add 'remember me' option
            var result = await signInManager.PasswordSignInAsync(
                user,
                password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded) //result may not succeed due to invalid 2FA code, not just incorrect password.
            {
                logger.LogWarning("Sign in failed for user: {UserId}. Result: {@SignInResult}", user.Id, result);
                throw new UnauthorizedAccessException("Your authentication attempt failed, please try again with valid credentials");
            }

            logger.LogDebug("Sign in successful for user: {UserId}", user.Id);
        }

        /// <summary>
        /// Retrieves the first role assigned to the user.
        /// </summary>
        /// <param name="user">The user whose role needs to be retrieved.</param>
        /// <returns>The user's first assigned role.</returns>
        /// <remarks>Assumes the user has at least one role assigned.</remarks>
        private async Task<string> GetUserRole(ApplicationUser user)
        {
            logger.LogDebug("Retrieving roles for user: {UserId}", user.Id);

            var userRoles = await userManager.GetRolesAsync(user);
            var role = userRoles.First()!;

            logger.LogDebug("Retrieved role {Role} for user: {UserId}", role, user.Id);
            return role;
        }

        /// <summary>
        /// Generates access, ID, and refresh tokens for the authenticated user.
        /// </summary>
        /// <param name="user">The user to generate tokens for.</param>
        /// <param name="userRole">The user's role to include in the access token.</param>
        /// <returns>A tuple containing the access token, ID token, and refresh token.</returns>
        /// <remarks>Updates the user's refresh token in the database.</remarks>
        private async Task<(string accessToken, string idToken, string refreshToken)> GenerateTokens(ApplicationUser user, string userRole)
        {
            logger.LogDebug("Generating tokens for user: {UserId} with role: {Role}", user.Id, userRole);

            string accessToken = accessTokenProvider.CreateToken(user, userRole);
            string idToken = idTokenProvider.CreateToken(user, userRole);
            string refreshToken = refreshTokenProvider.CreateToken(user);

            user.RefreshToken = refreshToken;
            await userManager.UpdateAsync(user);

            logger.LogDebug("Tokens generated successfully for user: {UserId}", user.Id);
            return (accessToken, idToken, refreshToken);
        }
    }
}