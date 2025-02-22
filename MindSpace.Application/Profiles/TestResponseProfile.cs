using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class TestResponseProfile : Profile
    {
        public TestResponseProfile() {
            CreateProjection<TestResponse, TestResponseOverviewResponseDTO>()
                .ForMember(d => d.TestScoreRankResult, a => a.MapFrom(tp => tp.TestScoreRankResult))
                .ForMember(d => d.TotalScore, a => a.MapFrom(tp => tp.TotalScore))
                .ForMember(d => d.Student, a => a.MapFrom(tp => tp.Student))
                .ForMember(d => d.Parent, a => a.MapFrom(tp => tp.Parent))
                .ForMember(d => d.Test, a => a.MapFrom(tp => tp.Test));
        }
    }
}
