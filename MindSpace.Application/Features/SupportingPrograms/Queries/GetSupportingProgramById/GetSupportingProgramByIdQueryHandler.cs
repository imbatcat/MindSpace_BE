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
    IMapper mapper) : IRequestHandler<GetSupportingProgramByIdQuery, SupportingProgramResponseDTO>
{
    public async Task<SupportingProgramResponseDTO> Handle(GetSupportingProgramByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get Supporting Program By Id: {@Id}", request.Id);

        var spec = new SupportingProgramSpecification(request.Id);

        var spFromDb = await unitOfWork.Repository<SupportingProgram>().GetBySpecAsync(spec)
            ?? throw new NotFoundException(nameof(SupportingProgram), request.Id.ToString());

        var result = mapper.Map<SupportingProgram, SupportingProgramResponseDTO>(spFromDb);

        return result;
    }
}