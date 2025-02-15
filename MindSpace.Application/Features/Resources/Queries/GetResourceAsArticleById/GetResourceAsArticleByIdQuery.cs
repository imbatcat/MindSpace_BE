using MediatR;
using MindSpace.Application.DTOs.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
