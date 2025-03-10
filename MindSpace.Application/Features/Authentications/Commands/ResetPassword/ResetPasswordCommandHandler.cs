using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Entities.Identity;
using System.Text;

namespace MindSpace.Application.Features.Authentications.Commands.ResetPassword
{
    internal class ResetPasswordCommandHandler(
        ILogger<ResetPasswordCommandHandler> logger,
        UserManager<ApplicationUser> userManager) : IRequestHandler<ResetPasswordCommand, bool>
    {
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Resetting password for user with email {email}", request.Email);

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                logger.LogWarning("User with email {email} not found", request.Email);
                return false;
            }

            var tokenBytes = Convert.FromBase64String(request.Token);
            var decodedToken = Encoding.UTF8.GetString(tokenBytes);

            var result = await userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            if (result.Succeeded)
            {
                logger.LogInformation("Password reset successfully for user {userEmail}", user.Email);
                return true;
            }

            logger.LogWarning("Password reset failed for user {userEmail}. Errors: {errors}",
                user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
            return false;
        }
    }
}