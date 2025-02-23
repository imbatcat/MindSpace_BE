using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Exceptions;

namespace MindSpace.Application.Features.Resources.Queries.GetResourceAsArticleById
{
    public class GetResourceAsArticleByIdQueryHandler : IRequestHandler<GetResourceAsArticleByIdQuery, ArticleResponseDTO>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetResourceAsArticleByIdQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================

        public GetResourceAsArticleByIdQueryHandler(ILogger<GetResourceAsArticleByIdQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<ArticleResponseDTO> Handle(GetResourceAsArticleByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Get Article: {request.Id}");

            var spec = new ResourceSpecification(request.Id, ResourceType.Article);
            var article = await _unitOfWork
                .Repository<Resource>()
                .GetBySpecProjectedAsync<ArticleResponseDTO>(spec, _mapper.ConfigurationProvider);

            if (article == null)
                throw new NotFoundException(nameof(Resource), request.Id.ToString());

            return _mapper.Map<ArticleResponseDTO>(article);
        }
    }
}
