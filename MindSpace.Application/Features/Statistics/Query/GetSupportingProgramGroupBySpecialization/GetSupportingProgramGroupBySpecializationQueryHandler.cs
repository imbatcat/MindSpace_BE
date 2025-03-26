using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Statistics.SupportingProgramStatistics;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.SpecializationSpecifications;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Features.Statistics.Query.GetSupportingProgramGroupBySpecialization
{
    public class GetSupportingProgramGroupBySpecializationQueryHandler(ILogger<GetSupportingProgramGroupBySpecializationQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper) : IRequestHandler<GetSupportingProgramGroupBySpecializationQuery, SupportingProgramsGroupBySpecializationDTO>
    {
        public async Task<SupportingProgramsGroupBySpecializationDTO> Handle(GetSupportingProgramGroupBySpecializationQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get statistics of supporting programs by specialization group analysis with Spec: {@Spec}", request);
            var specification = new SupportingProgramSpecification(request.SchoolId, request.StartDate, request.EndDate);

            var supportingPrograms = await unitOfWork.Repository<SupportingProgram>().GetAllWithSpecAsync(specification);

            int totalSupportingProgramCount = supportingPrograms.Count;

            IEnumerable<IGrouping<int, SupportingProgram>> groupedData = supportingPrograms.GroupBy(a => a.Psychologist.SpecializationId);
            List<SupportingProgramPairDTO> keyValuePairs = new();
            foreach (var group in groupedData)
            {
                Specialization? specializationEntity = await unitOfWork.Repository<Specialization>().GetBySpecAsync(new SpecializationSpecification(group.Key));
                SpecializationDTO specialization = mapper.Map<SpecializationDTO>(specializationEntity);
                keyValuePairs.Add(new SupportingProgramPairDTO { Specialization = specialization, SupportingProgramCount = group.ToList().Count });
            }
            return new SupportingProgramsGroupBySpecializationDTO
            {
                SchoolId = request.SchoolId,
                TotalSupportingProgramCount = totalSupportingProgramCount,
                KeyValuePairs = keyValuePairs
            };
        }
    }
}
