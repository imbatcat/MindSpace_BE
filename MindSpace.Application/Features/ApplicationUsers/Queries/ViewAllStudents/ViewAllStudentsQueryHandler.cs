using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Services.Authentication;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllStudents
{
    public class ViewAllStudentsQueryHandler(
        ILogger<ViewAllStudentsQueryHandler> logger,
        IApplicationUserRepository applicationUserService,
        IMapper mapper,
        IUserContext userContext
    ) : IRequestHandler<ViewAllStudentsQuery, PagedResultDTO<ApplicationUserResponseDTO>>
    {

        public async Task<PagedResultDTO<ApplicationUserResponseDTO>> Handle(ViewAllStudentsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all student accounts");
            var userClaims = userContext.GetCurrentUser();
            ApplicationUser? currentUser = applicationUserService.GetUserByEmailAsync(userClaims!.Email).Result;
            if (currentUser == null || currentUser.SchoolManager == null || request.SpecParams.SchoolId == null || currentUser.SchoolManager.SchoolId != request.SpecParams.SchoolId)
            {
                throw new AuthorizationFailedException("You are not authorized to view students of this school!");
            }
            var specification = new ApplicationUserSpecification(request.SpecParams, isOnlyStudent: true);
            var users = await applicationUserService.GetAllUsersWithSpecAsync(specification);

            logger.LogInformation("Found {Count} student users", users.Count);
            var userDtos = mapper.Map<List<ApplicationUserResponseDTO>>(users);

            foreach (var userDto in userDtos)
            {
                userDto.Role = UserRoles.Student;
            }

            return new PagedResultDTO<ApplicationUserResponseDTO>(userDtos.Count, userDtos);
        }
    }
}