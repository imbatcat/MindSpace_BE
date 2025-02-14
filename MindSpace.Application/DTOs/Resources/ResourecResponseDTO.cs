using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.DTOs.Resources
{
    public class ResourceResponseDTO
    {
        public ResourceType ResourceType { get; set; }
        public string ArticleUrl { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
