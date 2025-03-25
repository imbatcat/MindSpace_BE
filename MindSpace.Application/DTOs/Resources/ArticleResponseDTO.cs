namespace MindSpace.Application.DTOs.Resources
{
    public class ArticleResponseDTO
    {
        public int Id { get; set; }
        public string ResourceType { get; set; }
        public string ArticleUrl { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool isActive { get; set; }
        public string SpecializationName { get; set; }
        public string SchoolManagerName { get; set; }
    }
}
