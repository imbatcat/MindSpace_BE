using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Authentication.Commands.RegisterUser
{
    internal class RegisterUserCommandHandler
        (ILogger<RegisterUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore) : IRequestHandler<RegisterUserCommand>
    {
        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userEmailStore = (IUserEmailStore<ApplicationUser>)userStore;

            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException($"{nameof(Handle)} requires a user store with email support.");
            }
            var email = request.Email;
            var username = request.Username;

            var user = new ApplicationUser();
            await userStore.SetUserNameAsync(user, username, CancellationToken.None);
            await userEmailStore.SetEmailAsync(user, email, CancellationToken.None);

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new CreateUserFailedException(request.Email);
            }

            var role = request.IsStudent ? UserRoles.Student : UserRoles.Parent;
            await userManager.AddToRoleAsync(user, UserRoles.Student);

            logger.LogInformation("User {email} with {role} has beed created successfully", request.Email, role);
            //TODO: Send confirmation email here
        }
    }
}