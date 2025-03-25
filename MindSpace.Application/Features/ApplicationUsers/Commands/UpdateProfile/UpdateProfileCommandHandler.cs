using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.ApplicationUsers.Commands.UpdateProfile
{
    internal class UpdateProfileCommandHandler(
        ILogger<UpdateProfileCommandHandler> logger,
        IApplicationUserService<ApplicationUser> applicationUserService,
        IMapper mapper) : IRequestHandler<UpdateProfileCommand, ApplicationUserProfileDTO>
    {
        public async Task<ApplicationUserProfileDTO> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Attempting to update profile for user with ID: {UserId}", request.UserId);

            var user = await applicationUserService.GetByIdAsync(request.UserId);
            if (user == null)
            {
                logger.LogWarning("User not found with ID: {UserId}", request.UserId);
                return null!;
            }

            logger.LogInformation("Updating basic profile information for user {Email}", user.Email);
            user.PhoneNumber = request.PhoneNumber;
            user.DateOfBirth = request.BirthDate;
            user.ImageUrl = request.ImgUrl;

            var isUserPsychologist = false;
            if (user is Psychologist psychologist)
            {
                logger.LogInformation("Updating psychologist-specific information for user {Email}", user.Email);
                isUserPsychologist = true;
                psychologist.Bio = request.Bio!;
                psychologist.AverageRating = request.AverageRating ?? 0;
                psychologist.SessionPrice = request.SessionPrice ?? 0;
                psychologist.ComissionRate = request.ComissionRate ?? 0;
            }

            await applicationUserService.UpdateAsync(user);
            logger.LogInformation("Successfully updated profile for user {Email}", user.Email);

            return isUserPsychologist ? mapper.Map<PsychologistProfileDTO>(user) : mapper.Map<ApplicationUserProfileDTO>(user);
        }
    }
}
