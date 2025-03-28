using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Statistics;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Specifications.ApplicationUserSpecifications;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Application.Specifications.TestSpecifications;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Features.Statistics.Query.GetOverviewStatistics
{
    public class GetOverviewStatisticsQueryHandler(ILogger<GetOverviewStatisticsQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IApplicationUserService<ApplicationUser> applicationUserRepository) : IRequestHandler<GetOverviewStatisticsQuery, CountSchoolOverviewDataDTO>
    {
        public async Task<CountSchoolOverviewDataDTO> Handle(GetOverviewStatisticsQuery request, CancellationToken cancellationToken)
        {
            int totalStudent = await applicationUserRepository.CountAsync(new ApplicationUserSpecification(request.SchoolId, request.StartDate, request.EndDate));

            int totalTests = await unitOfWork.Repository<Test>().CountAsync(new TestSpecification(request.SchoolId, null, request.StartDate, request.EndDate));

            int totalSupportingPrograms = await unitOfWork.Repository<SupportingProgram>().CountAsync(new SupportingProgramSpecification(request.SchoolId, request.StartDate, request.EndDate));

            int totalResources = await unitOfWork.Repository<Resource>().CountAsync(new ResourceSpecification(request.SchoolId, request.StartDate, request.EndDate));
            return new CountSchoolOverviewDataDTO
            {
                TotalStudentsCount = totalStudent,
                TotalTestsCount = totalTests,
                TotalResourcesCount = totalResources,
                TotalSupportingProgramCount = totalSupportingPrograms,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
        }
    }
}
