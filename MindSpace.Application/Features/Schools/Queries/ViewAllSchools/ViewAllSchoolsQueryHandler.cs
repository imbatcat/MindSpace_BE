using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.SchoolSpecifications;
using MindSpace.Domain.Entities;

namespace MindSpace.Application.Features.Schools.Queries.ViewAllSchools
{
    public class ViewAllSchoolsQueryHandler(
        ILogger<ViewAllSchoolsQueryHandler> _logger,
        IUnitOfWork _unitOfWork,
        IMapper _mapper
    ) : IRequestHandler<ViewAllSchoolsQuery, List<SchoolDTO>>
    {
        public async Task<List<SchoolDTO>> Handle(ViewAllSchoolsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all schools");

            // Create an empty specification to get all schools
            var spec = new SchoolSpecifications();
            var schoolDtos = await _unitOfWork.Repository<School>().GetAllWithSpecProjectedAsync<SchoolDTO>(spec, _mapper.ConfigurationProvider);

            _logger.LogInformation("Found {Count} schools", schoolDtos.Count);
            return schoolDtos.ToList();
        }
    }
}