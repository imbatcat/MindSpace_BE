using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Entities.Identity;
using System.Text;

namespace MindSpace.Application.Features.Authentications.Commands.ConfirmEmail
{
    internal class ConfirmEmailCommandHandler(
        ILogger<ConfirmEmailCommandHandler> logger,
        UserManager<ApplicationUser> userManager) : IRequestHandler<ConfirmEmailCommand, bool>
    {
        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Confirming email for user with email {email}", request.Email);

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                logger.LogWarning("User with email {email} not found", request.Email);
                return false;
            }

            var tokenBytes = Convert.FromBase64String(request.ConfirmationToken);
            var decodedToken = Encoding.UTF8.GetString(tokenBytes);

            var result = await userManager.ConfirmEmailAsync(user, decodedToken);

            if (result.Succeeded)
            {
                logger.LogInformation("Email confirmed successfully for user {userEmail}", user.Email);
                return true;
            }

            logger.LogWarning("Email confirmation failed for user {userEmail}. Errors: {errors}",
                user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
            return false;
        }
    }
}