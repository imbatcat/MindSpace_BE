using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Interfaces.Services.Authentication;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewProfileById
{
    public class ViewProfileByIdQueryHandler(
        ILogger<ViewProfileByIdQueryHandler> logger,
        IApplicationUserService applicationUserService,
        IMapper mapper
    ) : IRequestHandler<ViewProfileByIdQuery, ApplicationUserProfileDTO>
    {
        public async Task<ApplicationUserProfileDTO> Handle(ViewProfileByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting user profile for user ID: {UserId}", request.UserId);
            var user = await applicationUserService.GetByIdAsync(request.UserId);

            if (user == null)
            {
                logger.LogWarning("User not found with ID: {UserId}", request.UserId);
                return null!;
            }

            if (user is Psychologist psychologist)
            {
                logger.LogInformation("Mapping psychologist profile for user {Email}", user.Email);
                return mapper.Map<PsychologistProfileDTO>(user);
            }

            logger.LogInformation("Mapping regular user profile for user {Email}", user.Email);
            return mapper.Map<ApplicationUserProfileDTO>(user);
        }
    }
}
