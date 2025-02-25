using AutoMapper;
using MindSpace.Application.DTOs.Resources;
using MindSpace.Application.Features.Resources.Commands.CreateResourceAsArticle;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;

namespace MindSpace.Application.Profiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            // =============================
            // === GET
            // =============================

            CreateProjection<Resource, ArticleResponseDTO>()
                .ForMember(d => d.SpecializationName, o => o.MapFrom(m => m.Specialization.Name))
                .ForMember(d => d.SchoolManagerName, o => o.MapFrom(m => m.SchoolManager.FullName));

            CreateMap<Resource, ArticleResponseDTO>()
                .ForMember(d => d.SpecializationName, o => o.MapFrom(m => m.Specialization.Name))
                .ForMember(d => d.SchoolManagerName, o => o.MapFrom(m => m.SchoolManager.FullName));

            CreateProjection<Resource, BlogResponseDTO>()
                .ForMember(d => d.SpecializationName, o => o.MapFrom(m => m.Specialization.Name))
                .ForMember(d => d.SchoolManagerName, o => o.MapFrom(m => m.SchoolManager.FullName))
                .ForMember(d => d.Sections, o => o.MapFrom(m => m.ResourceSections));

            CreateMap<Resource, BlogResponseDTO>()
                .ForMember(d => d.SpecializationName, o => o.MapFrom(m => m.Specialization.Name))
                .ForMember(d => d.SchoolManagerName, o => o.MapFrom(m => m.SchoolManager.FullName))
                .ForMember(d => d.Sections, o => o.MapFrom(m => m.ResourceSections));

            CreateProjection<ResourceSection, SectionResponseDTO>();
            CreateMap<ResourceSection, SectionResponseDTO>();

            // =====================================
            // === PATCH, POST
            // =====================================

            CreateMap<BlogDraft, Resource>()
                .ForMember(d => d.Id, o => o.Ignore()) // Ignore the id from the temp blog
                .ForMember(d => d.SchoolManagerId, o => o.MapFrom(s => s.SchoolManagerId ?? 0))
                .ForMember(d => d.SpecializationId, o => o.MapFrom(s => s.SpecializationId ?? 0))
                .ForMember(d => d.ResourceType, o => o.MapFrom(_ => ResourceType.Blog))
                .ForMember(d => d.ArticleUrl, o => o.Ignore())
                .ForMember(d => d.ResourceSections, o => o.MapFrom(s => s.Sections));

            CreateMap<SectionDraft, ResourceSection>()
                .ForMember(d => d.Id, o => o.Ignore()); // Set Manually

            CreateMap<CreatedResourceAsArticleCommand, Resource>()
                .ForMember(d => d.isActive, o => o.MapFrom(s => true))
                .ForMember(d => d.ResourceType, o => o.MapFrom(s => ResourceType.Article));
        }
    }
}
