using MediatR;

namespace MindSpace.Application.Features.Resources.Commands.CreateResourceAsArticle
{
    public class CreatedResourceAsArticleCommand : IRequest
    {
        public string ArticleUrl { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string ThumbnailUrl { get; set; }
        public int SchoolManagerId { get; set; }
        public int SpecializationId { get; set; }
    }
}
