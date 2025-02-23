using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Interfaces.Services.Authentication;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllAccounts
{
    public class ViewAllAccountsQueryHandler(
        ILogger<ViewAllAccountsQueryHandler> logger,
        IApplicationUserService applicationUserService,
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IMapper mapper
    ) : IRequestHandler<ViewAllAccountsQuery, PagedResultDTO<ApplicationUserResponseDTO>>
    {
        public async Task<PagedResultDTO<ApplicationUserResponseDTO>> Handle(ViewAllAccountsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all user accounts");

            var specification = new ApplicationUserSpecification(request.SpecParams);

            // Get users with the specification
            var users = await applicationUserService.GetAllUsersWithSpecAsync(specification);
            int count = 0;

            // If role filtering is requested, filter the users by role
            if (!string.IsNullOrEmpty(request.SpecParams.RoleId))
            {
                var role = UserRoles.RoleMap[request.SpecParams.RoleId];
                if (role != null)
                {
                    var usersInRole = await userManager.GetUsersInRoleAsync(role);
                    users = users.Where(u => usersInRole.Any(ur => ur.Id == u.Id)).ToList();
                    count = users.Count;
                }
            }
            else
            {
                count = await applicationUserService.CountAsync(specification);
            }

            logger.LogInformation("Found {Count} users", users.Count);

            var userDtos = mapper.Map<List<ApplicationUserResponseDTO>>(users);

            // Add role information to each user DTO
            foreach (var userDto in userDtos)
            {
                var user = users.First(u => u.Id == userDto.Id);
                var roles = await userManager.GetRolesAsync(user);
                userDto.Role = roles.FirstOrDefault() ?? string.Empty;
            }

            return new PagedResultDTO<ApplicationUserResponseDTO>(count, userDtos);
        }
    }
}