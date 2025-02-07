using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Authentication.Services
{
    public class UserRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly ILogger<UserRegistrationService> _logger;

        public UserRegistrationService(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            ILogger<UserRegistrationService> logger)
        {
            _userManager = userManager;
            _userStore = userStore;
            _logger = logger;
        }

        public async Task<ApplicationUser> RegisterUserAsync(
            RegisterUserDTO registerUserDTO,
            CancellationToken cancellationToken = default)
        {
            var userEmailStore = (IUserEmailStore<ApplicationUser>)_userStore;

            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("User registration requires a user store with email support.");
            }

            var user = new ApplicationUser();
            await _userStore.SetUserNameAsync(user, registerUserDTO.UserName, cancellationToken);
            await userEmailStore.SetEmailAsync(user, registerUserDTO.Email, cancellationToken);

            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);
            if (!result.Succeeded)
            {
                throw new CreateUserFailedException(registerUserDTO.Email);
            }

            await _userManager.AddToRoleAsync(user, registerUserDTO.Role);
            _logger.LogInformation("User {email} with role {role} has been created successfully", registerUserDTO.Email, registerUserDTO.Role);

            return user;
        }

        public async Task<IEnumerable<ApplicationUser>> RegisterUserAsync(IEnumerable<RegisterUserDTO> userDTOs,
        CancellationToken cancellationToken = default)
        {
            var userEmailStore = (IUserEmailStore<ApplicationUser>)_userStore;

            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("User registration requires a user store with email support.");
            }

            var users = new List<ApplicationUser>();
            foreach (var userDTO in userDTOs)
            {
                var user = new ApplicationUser();
                await _userStore.SetUserNameAsync(user, userDTO.UserName, cancellationToken);
                await userEmailStore.SetEmailAsync(user, userDTO.Email, cancellationToken);

                var result = await _userManager.CreateAsync(user, userDTO.Password);
                if (!result.Succeeded)
                {
                    throw new CreateUserFailedException(userDTO.Email);
                }

                await _userManager.AddToRoleAsync(user, userDTO.Role);
                _logger.LogInformation("User {email} with role {role} has been created successfully", userDTO.Email, userDTO.Role);

                users.Add(user);
            }

            return users;
        }
    }

    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}