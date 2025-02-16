using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;

namespace MindSpace.Application.Features.Resources.Queries.GetArticles
{
    public class GetArticlesQuery : IRequest<PagedResultDTO<ArticleResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        public ResourceSpecificationSpecParams SpecParams { get; private set; }

        // ================================
        // === Constructors
        // ================================

        public GetArticlesQuery(ResourceSpecificationSpecParams specParams)
        {
            SpecParams = specParams;
            SpecParams.Type = ResourceType.Article.ToString();
        }
    }
}
