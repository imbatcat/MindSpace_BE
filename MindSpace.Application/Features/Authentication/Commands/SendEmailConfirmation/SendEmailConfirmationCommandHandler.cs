using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.EmailServices;
using MindSpace.Domain.Entities.Identity;
using System.Text;

namespace MindSpace.Application.Features.Authentication.Commands.SendEmailConfirmation
{
    internal class SendEmailConfirmationCommandHandler(
        ILogger<SendEmailConfirmationCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        IEmailService emailService) : IRequestHandler<SendEmailConfirmationCommand>
    {
        public async Task Handle(SendEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Generating email confirmation token for user {userEmail}", request.User.Email);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(request.User);
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            var confirmationLink = $"https://localhost:7096/api/v1/identity/confirm-email?token={encodedToken}&email={request.User.Email}";

            var emailSubject = "Confirm your email address";
            var emailContent = $@"
                <h2>Welcome to MindSpace!</h2>
                <p>Please confirm your email address by clicking the link below:</p>
                <p><a href='{confirmationLink}'>Confirm Email</a></p>
                <p>If you did not create an account, please ignore this email.</p>
                <p>Thank you,<br>The MindSpace Team</p>";

            await emailService.SendEmailAsync(request.User.Email!, emailSubject, emailContent);

            logger.LogInformation("Email confirmation sent to user {userEmail}", request.User.Email);
        }
    }
}