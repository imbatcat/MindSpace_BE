using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.Authentication.DTOs;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Domain.Services.Authentication;

namespace MindSpace.Application.Features.Authentication.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService<ApplicationUser> _applicationUserService;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly ILogger<UserRegistrationService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserRegistrationService(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            ILogger<UserRegistrationService> logger,
            IApplicationUserService<ApplicationUser> applicationUserService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _userStore = userStore;
            _logger = logger;
            _applicationUserService = applicationUserService;
            _unitOfWork = unitOfWork;
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

        public async Task SaveSchoolManagerAndSchoolAsync(SchoolManager manager, School school, CancellationToken cancellationToken = default)
        {
            // Start a transaction to ensure both entities are saved together
            try
            {
                // Add both entities
                //await _applicationUserService;
                //await _unitOfWork.Schools.AddAsync(school, cancellationToken);

                // Save changes
                await _unitOfWork.CompleteAsync();

                // Commit the transaction
                _logger.LogInformation("School {schoolName} and its manager {managerId} have been saved successfully",
                    school.SchoolName, manager.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save school {schoolName} and its manager {managerId}",
                    school.SchoolName, manager.Id);
                throw;
            }
        }
    }
}