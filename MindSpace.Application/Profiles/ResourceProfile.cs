using AutoMapper;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Profiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<BlogDraft, Resource>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.SchoolManagerId, o => o.MapFrom(s => s.SchoolManagerId ?? 0))
                .ForMember(d => d.SpecializationId, o => o.MapFrom(s => s.SpecializationId ?? 0))
                .ForMember(d => d.ResourceType, o => o.MapFrom(_ => ResourceType.Blog))
                .ForMember(d => d.ArticleUrl, o => o.Ignore())
                .ForMember(d => d.ResourceSections, o => o.MapFrom(s => s.Sections));
        }
    }
}
