using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Resources.Commands.CreateResourceAsArticle;

public class CreateResourceAsArticleCommandHandler(
    ILogger<CreatedResourceAsArticleCommand> logger,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreatedResourceAsArticleCommand, ArticleResponseDTO>
{
    public async Task<ArticleResponseDTO> Handle(CreatedResourceAsArticleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create Resource with Title: {@title}", request.Title);

        var articleToCreate = mapper.Map<Resource>(request);
        _ = unitOfWork.Repository<Resource>().Insert(articleToCreate)
            ?? throw new UpdateFailedException(nameof(Resource));

        await unitOfWork.CompleteAsync();

        return mapper.Map<Resource, ArticleResponseDTO>(articleToCreate);
    }
}
