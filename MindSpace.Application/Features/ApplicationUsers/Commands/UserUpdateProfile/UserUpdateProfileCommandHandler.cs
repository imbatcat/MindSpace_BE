using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Application.Interfaces.Services.ImageServices;
using MindSpace.Domain.Entities.Identity;
namespace MindSpace.Application.Features.ApplicationUsers.Commands.UserUpdateProfile
{
    internal class UserUpdateProfileCommandHandler(
        ILogger<UserUpdateProfileCommandHandler> logger,
        IApplicationUserService<ApplicationUser> applicationUserService,
        IUserContext userContext,
        IImageService imageService,
        IMapper mapper) : IRequestHandler<UserUpdateProfileCommand, ApplicationUserProfileDTO>
    {
        public async Task<ApplicationUserProfileDTO> Handle(UserUpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var userClaims = userContext.GetCurrentUser();
            logger.LogInformation("Attempting to update profile for user: {userEmail}", userClaims.Email!);

            var userFromDatabase = await applicationUserService.GetUserByEmailAsync(userClaims.Email);
            if (userFromDatabase == null)
            {
                logger.LogWarning("User not found with ID: {UserId}", userFromDatabase);
                return null!;
            }

            logger.LogInformation("Updating basic profile information for user {Email}", userFromDatabase.Email);
            userFromDatabase.PhoneNumber = request.PhoneNumber;
            userFromDatabase.DateOfBirth = request.BirthDate;
            userFromDatabase.ImageUrl = request.Image != null ? await UploadImage() : userFromDatabase.ImageUrl;

            var isUserPsychologist = false;
            if (userFromDatabase is Psychologist psychologist)
            {
                logger.LogInformation("Updating psychologist-specific information for user {Email}", userFromDatabase.Email);
                isUserPsychologist = true;
                psychologist.Bio = request.Bio!;
                psychologist.SessionPrice = request.SessionPrice > 0 ? (request.SessionPrice <= 200000 ? (decimal)request.SessionPrice : throw new Exception("Session price must be between 0 and 200000")) : throw new Exception("Session price must be greater than 0");
            }

            await applicationUserService.UpdateAsync(userFromDatabase);
            logger.LogInformation("Successfully updated profile for user {Email}", userFromDatabase.Email);

            return isUserPsychologist ? mapper.Map<PsychologistProfileDTO>(userFromDatabase) : mapper.Map<ApplicationUserProfileDTO>(userFromDatabase);

            async Task<string> UploadImage()
            {
                var imgUrl = await imageService.UploadImage(new ImageUploadParams
                {
                    File = new FileDescription(request.Image.FileName, request.Image.OpenReadStream()),
                });
                return imgUrl;
            }
        }
    }
}
