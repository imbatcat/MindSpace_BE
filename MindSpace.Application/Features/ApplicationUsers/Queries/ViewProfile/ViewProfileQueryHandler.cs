using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.UserContext;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;
using MindSpace.Domain.Interfaces.Services.Authentication;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewProfile
{
    internal class ViewProfileQueryHandler
    (ILogger<ViewProfileQueryHandler> logger,
    IApplicationUserService applicationUserService,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<ViewProfileQuery, ApplicationUserProfileDTO>
    {
        public async Task<ApplicationUserProfileDTO> Handle(ViewProfileQuery request, CancellationToken cancellationToken)
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