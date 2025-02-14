using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Drafts.TestPeriodics;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class QuestionOptionProfile : Profile
    {
        public QuestionOptionProfile()
        {
            CreateProjection<QuestionOption, QuestionOptionResponseDTO>();
            CreateMap<OptionDraft, QuestionOption>()
                .ForMember(d => d.Id, a => a.Ignore())
                .ForMember(d => d.DisplayedText, a => a.MapFrom(o => o.DisplayedText));

        }
    }
}