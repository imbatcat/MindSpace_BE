using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Specifications.ResourceSpecifications;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Features.Resources.Queries.GetBlogs
{
    public class GetBlogsQuery : IRequest<PagedResultDTO<BlogResponseDTO>>
    {
        // ================================
        // === Fields & Props
        // ================================

        public ResourceSpecificationSpecParams SpecParams { get; private set; }

        // ================================
        // === Constructors
        // ================================

        public GetBlogsQuery(ResourceSpecificationSpecParams specParams)
        {
            SpecParams = specParams;
            SpecParams.Type = ResourceType.Blog.ToString();
        }
    }
}