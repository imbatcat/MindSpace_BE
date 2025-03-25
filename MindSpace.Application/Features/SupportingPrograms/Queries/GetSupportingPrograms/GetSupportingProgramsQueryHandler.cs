using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingPrograms;

public class GetSupportingProgramQueryHandler(
    ILogger<GetSupportingProgramQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetSupportingProgramsQuery, PagedResultDTO<SupportingProgramResponseDTO>>
{
    public async Task<PagedResultDTO<SupportingProgramResponseDTO>> Handle(GetSupportingProgramsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get list of Supporting Programs with Spec: {@Spec}", request.SpecParams);

        var spec = new SupportingProgramSpecification(request.SpecParams);

        // Use Projection
        var listDto = await unitOfWork
            .Repository<SupportingProgram>()
            .GetAllWithSpecProjectedAsync<SupportingProgramResponseDTO>(spec, mapper.ConfigurationProvider);

        var count = await unitOfWork
            .Repository<SupportingProgram>()
            .CountAsync(spec);

        return new PagedResultDTO<SupportingProgramResponseDTO>(count, listDto);
    }
}