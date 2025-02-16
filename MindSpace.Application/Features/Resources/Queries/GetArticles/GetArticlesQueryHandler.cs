using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Interfaces.Repos;

namespace MindSpace.Application.Features.Resources.Queries.GetArticles
{
    public class GetArticlesQueryHandler
    : IRequestHandler<GetArticlesQuery, PagedResultDTO<ArticleResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        private readonly ILogger<GetArticlesQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // ================================
        // === Constructors
        // ================================
        public GetArticlesQueryHandler(
            ILogger<GetArticlesQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // ================================
        // === Methods
        // ================================

        public async Task<PagedResultDTO<ArticleResponseDTO>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get list of Articles: {@spec}", request.SpecParams);
            var spec = new ResourceSpecification(request.SpecParams);

            // Use Projection 
            var listDto = await _unitOfWork
                .Repository<Resource>()
                .GetAllWithSpecProjectedAsync<ArticleResponseDTO>(spec, _mapper.ConfigurationProvider);

            var count = await _unitOfWork
                .Repository<Resource>()
                .CountAsync(spec);

            return new PagedResultDTO<ArticleResponseDTO>(count, listDto);
        }
    }
}
