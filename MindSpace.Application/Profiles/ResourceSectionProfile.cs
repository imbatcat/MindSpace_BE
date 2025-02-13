using AutoMapper;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
