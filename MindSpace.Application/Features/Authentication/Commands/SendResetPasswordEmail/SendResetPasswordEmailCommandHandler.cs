using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.EmailServices;
using MindSpace.Domain.Entities.Identity;
using System.Text;

namespace MindSpace.Application.Features.Authentication.Commands.SendResetPasswordEmail
{
    internal class SendResetPasswordEmailCommandHandler(
        ILogger<SendResetPasswordEmailCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        IEmailService emailService) : IRequestHandler<SendResetPasswordEmailCommand>
    {
        public async Task Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Generating password reset token for user {userEmail}", request.Email);

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                logger.LogWarning("User with email {email} not found", request.Email);
                return;
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            var resetLink = $"https://localhost:7096/api/v1/identity/reset-password?token={encodedToken}&email={request.Email}";

            var emailSubject = "Reset Your Password";
            var emailContent = $@"
                <h2>Reset Your Password</h2>
                <p>You have requested to reset your password. Click the link below to set a new password:</p>
                <p><a href='{resetLink}'>Reset Password</a></p>
                <p>If you did not request a password reset, please ignore this email or contact support if you have concerns.</p>
                <p>This link will expire in 24 hours.</p>
                <p>Thank you,<br>The MindSpace Team</p>";

            await emailService.SendEmailAsync(request.Email, emailSubject, emailContent);

            logger.LogInformation("Password reset email sent to user {userEmail}", request.Email);
        }
    }
}