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
using StackExchange.Redis;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllAccounts
{
    public class ViewAllAccountsQueryHandler(
        ILogger<ViewAllAccountsQueryHandler> logger,
        IApplicationUserRepository applicationUserService,
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IMapper mapper
    ) : IRequestHandler<ViewAllAccountsQuery, PagedResultDTO<ApplicationUserResponseDTO>>
    {
        public async Task<PagedResultDTO<ApplicationUserResponseDTO>> Handle(ViewAllAccountsQuery request, CancellationToken cancellationToken)
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(request.SpecParams.RoleId))
            {
                var specification = new ApplicationUserSpecification(request.SpecParams);
                users = (List<ApplicationUser>)await applicationUserService.GetAllUsersWithSpecAsync(specification);
            }
            else
            {
                var role = UserRoles.RoleMap[request.SpecParams.RoleId];
                users = await applicationUserService.FilterUserByRoleAsync(role, request.SpecParams);
            }
            int count = users.Count;
            var userDtos = mapper.Map<List<ApplicationUserResponseDTO>>(users);
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