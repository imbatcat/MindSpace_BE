using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Statistics.SystemStatistics;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.SchoolSpecifications;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Features.Statistics.Query.GetOverviewSystemStatistics
{
    public class GetOverviewSystemStatisticsQueryHandler(ILogger<GetOverviewSystemStatisticsQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IApplicationUserService<ApplicationUser> applicationUserService) : IRequestHandler<GetOverviewSystemStatisticsQuery, OverviewStatisticsDTO>
    {
        public async Task<OverviewStatisticsDTO> Handle(GetOverviewSystemStatisticsQuery request, CancellationToken cancellationToken)
        {
            int totalSchools = await unitOfWork.Repository<School>().CountAsync(new SchoolSpecifications(request.StartDate, request.EndDate));

            int totalPsychologists = await applicationUserService.CountUserByRoleAsync(UserRoles.Psychologist, request.StartDate, request.EndDate);

            int totalParents = await applicationUserService.CountUserByRoleAsync(UserRoles.Parent, request.StartDate, request.EndDate);

            var result = new OverviewStatisticsDTO { TotalSchools = totalSchools, TotalPsychologists = totalPsychologists, TotalParents = totalParents };

            return result;

        }
    }
}
