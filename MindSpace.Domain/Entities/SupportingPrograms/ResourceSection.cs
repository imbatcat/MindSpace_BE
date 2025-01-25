namespace MindSpace.Domain.Entities.SupportingPrograms
{
    public class ResourceSection : BaseEntity
    {

        public string Heading { get; set; }
        public string HtmlContent { get; set; }


        // 1 Resource - M ResourceSection
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
