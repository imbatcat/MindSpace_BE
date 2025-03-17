namespace MindSpace.Domain.Entities.Drafts.Blogs
{
    public class SectionDraft
    {
        public required int Id { get; set; }
        public string Heading { get; set; }
        public string HtmlContent { get; set; }
    }
}
