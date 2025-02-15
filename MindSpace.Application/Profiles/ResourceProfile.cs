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

            CreateMap<Resource, ResourceResponseDTO>();

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

            CreateMap<CreatedResourceAsArticleCommand, Resource>()
                .ForMember(d => d.isActive, o => o.MapFrom(s => true))
                .ForMember(d => d.ResourceType, o => o.MapFrom(s => ResourceType.Article));
        }
    }
}
