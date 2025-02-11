using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Resources
{
    public class Resource : BaseEntity
    {
        public ResourceType ResourceType { get; set; }
        public string ArticleUrl { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string ThumbnailUrl { get; set; }

        // 1 SchoolManager - M Resources
        public int SchoolManagerId { get; set; }
        public SchoolManager SchoolManager { get; set; }


        // 1 Specialization - M Resources
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }


        // 1 Resource - M ResourceSection
        public virtual ICollection<ResourceSection> ResourceSections { get; set; } = new List<ResourceSection>();
    }
}
