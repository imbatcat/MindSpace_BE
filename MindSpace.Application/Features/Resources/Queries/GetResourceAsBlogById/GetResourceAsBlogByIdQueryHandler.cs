using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Resources.Queries.GetResourceAsBlogById;

public class GetResourceAsBlogByIdQueryHandler(
    ILogger<GetResourceAsBlogByIdQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetResourceAsBlogByIdQuery, BlogResponseDTO>
{
    public async Task<BlogResponseDTO> Handle(GetResourceAsBlogByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get Blog: {request.Id}");

        var spec = new ResourceSpecification(request.Id, ResourceType.Blog);
        var blog = await unitOfWork
            .Repository<Resource>()
            .GetBySpecProjectedAsync<BlogResponseDTO>(spec, mapper.ConfigurationProvider);

        if (blog == null)
            throw new NotFoundException(nameof(Resource), request.Id.ToString());

        return mapper.Map<BlogResponseDTO>(blog);
    }
}
