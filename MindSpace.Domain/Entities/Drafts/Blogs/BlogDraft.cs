namespace MindSpace.Domain.Entities.Drafts.Blogs
{
    public class BlogDraft
    {
        public required string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Introduction { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public int? SchoolManagerId { get; set; } = null;
        public int? SpecializationId { get; set; } = null;

        public List<SectionDraft> Sections { get; set; } = new List<SectionDraft>();
    }
}
