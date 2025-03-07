using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.SupportingProgramSpecifications;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.SupportingPrograms.Queries.GetSupportingProgramById;

public class GetSupportingProgramByIdQueryHandler(
    ILogger<GetSupportingProgramByIdQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetSupportingProgramByIdQuery, SupportingProgramWithStudentsResponseDTO>
{
    public async Task<SupportingProgramWithStudentsResponseDTO> Handle(GetSupportingProgramByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get Supporting Program By Id: {@Id}", request.Id);

        var spec = new SupportingProgramSpecification(request.Id);

        var dataDto = await unitOfWork
            .Repository<SupportingProgram>()
            .GetBySpecProjectedAsync<SupportingProgramWithStudentsResponseDTO>(spec, mapper.ConfigurationProvider);

        if (dataDto == null)
        {
            throw new NotFoundException(nameof(SupportingProgram), request.Id.ToString());
        }

        return dataDto;
    }
}