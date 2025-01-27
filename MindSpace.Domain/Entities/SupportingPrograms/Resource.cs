using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.SupportingPrograms
{
    public class Resource : BaseEntity
    {
        public ResourceType ResourceType { get; set; }
        public string ArticleUrl { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string ThumbnailUrl { get; set; }


        // 1 Manager - M Resources
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }


        // 1 Resource - M ResourceSection
        public virtual ICollection<ResourceSection> ResourceSections { get; set; } = new List<ResourceSection>();
    }
}
