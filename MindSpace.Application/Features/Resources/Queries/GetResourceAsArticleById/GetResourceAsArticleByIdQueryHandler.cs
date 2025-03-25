using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Resources.Queries.GetResourceAsArticleById;

public class GetResourceAsArticleByIdQueryHandler(
    ILogger<GetResourceAsArticleByIdQueryHandler> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetResourceAsArticleByIdQuery, ArticleResponseDTO>
{
    public async Task<ArticleResponseDTO> Handle(GetResourceAsArticleByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get Article: {request.Id}");

        var spec = new ResourceSpecification(request.Id, ResourceType.Article);
        var article = await unitOfWork
            .Repository<Resource>()
            .GetBySpecProjectedAsync<ArticleResponseDTO>(spec, mapper.ConfigurationProvider);

        if (article == null)
            throw new NotFoundException(nameof(Resource), request.Id.ToString());

        return mapper.Map<ArticleResponseDTO>(article);
    }
}
