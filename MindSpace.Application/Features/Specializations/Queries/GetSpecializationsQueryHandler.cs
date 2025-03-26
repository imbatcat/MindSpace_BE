using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.SpecializationSpecifications;
using MindSpace.Domain.Entities;

namespace MindSpace.Application.Features.Specializations.Queries
{
    public class GetSpecializationsQueryHandler(
        ILogger<GetSpecializationsQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetSpecializationsQuery, PagedResultDTO<SpecializationDTO>>
    {
        public async Task<PagedResultDTO<SpecializationDTO>> Handle(GetSpecializationsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get list of Specializations with Spec: {@Spec}", request.SpecParams);

            var spec = new SpecializationSpecification(request.SpecParams);

            // Use Projection
            var listDto = await unitOfWork
                .Repository<Specialization>()
                .GetAllWithSpecProjectedAsync<SpecializationDTO>(spec, mapper.ConfigurationProvider);

            var count = await unitOfWork
                .Repository<Specialization>()
                .CountAsync(spec);

            return new PagedResultDTO<SpecializationDTO>(count, listDto);
        }
    }
}
