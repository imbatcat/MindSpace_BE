using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.GetMyProfile
{
    internal class GetMyProfileQueryHandler
    (ILogger<GetMyProfileQueryHandler> logger,
    IApplicationUserService<ApplicationUser> applicationUserService,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<GetMyProfileQuery, ApplicationUserProfileDTO>
    {
        public async Task<ApplicationUserProfileDTO> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            var userClaims = userContext.GetCurrentUser();
            var user = await applicationUserService.GetUserByEmailAsync(userClaims!.Email);
            logger.LogInformation("Viewing profile for user {Email}", user!.Email);

            if (user == null)
            {
                logger.LogError("User not found");
                throw new NotFoundException(nameof(ApplicationUser), user!.Email!);
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