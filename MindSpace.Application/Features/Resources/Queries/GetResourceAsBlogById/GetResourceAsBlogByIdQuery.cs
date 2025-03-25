using MediatR;
using MindSpace.Application.DTOs.Resources;

namespace MindSpace.Application.Features.Resources.Queries.GetResourceAsBlogById
{
    public class GetResourceAsBlogByIdQuery : IRequest<BlogResponseDTO>
    {
        public int Id { get; }
        public GetResourceAsBlogByIdQuery(int id)
        {
            Id = id;
        }
    }
}