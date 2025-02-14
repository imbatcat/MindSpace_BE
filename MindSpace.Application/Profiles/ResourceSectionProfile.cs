using AutoMapper;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;

namespace MindSpace.Application.Profiles
{
    public class ResourceSectionProfile : Profile
    {
        public ResourceSectionProfile()
        {
            CreateMap<SectionDraft, ResourceSection>()
                .ForMember(d => d.Id, o => o.Ignore()); // Set Manually
        }
    }
}
