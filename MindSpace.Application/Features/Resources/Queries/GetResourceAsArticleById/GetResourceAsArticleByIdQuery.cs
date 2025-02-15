using MediatR;
using MindSpace.Application.DTOs.Resources;

namespace MindSpace.Application.Features.Resources.Queries.GetResourceAsArticleById
{
    public class GetResourceAsArticleByIdQuery : IRequest<ArticleResponseDTO>
    {
        public int Id { get; private set; }

        public GetResourceAsArticleByIdQuery(int id)
        {
            Id = id;
        }
    }
}
