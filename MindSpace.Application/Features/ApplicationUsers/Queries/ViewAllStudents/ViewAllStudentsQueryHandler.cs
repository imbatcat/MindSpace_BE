using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Interfaces.Services.Authentication;

namespace MindSpace.Application.Features.ApplicationUsers.Queries.ViewAllStudents
{
    public class ViewAllStudentsQueryHandler(
        ILogger<ViewAllStudentsQueryHandler> logger,
        IApplicationUserService applicationUserService,
        IMapper mapper
    ) : IRequestHandler<ViewAllStudentsQuery, PagedResultDTO<ApplicationUserResponseDTO>>
    {
        public async Task<PagedResultDTO<ApplicationUserResponseDTO>> Handle(ViewAllStudentsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all student accounts");
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