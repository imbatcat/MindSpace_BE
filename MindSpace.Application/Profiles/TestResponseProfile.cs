using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Application.Features.TestResponses.Commands.CreateTestResponse;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class TestResponseProfile : Profile
    {
        public TestResponseProfile()
        {
            // Projection
            CreateProjection<TestResponse, TestResponseOverviewResponseDTO>()
                .ForMember(d => d.TestScoreRankResult, a => a.MapFrom(tp => tp.TestScoreRankResult))
                .ForMember(d => d.TotalScore, a => a.MapFrom(tp => tp.TotalScore))
                .ForMember(d => d.Student, a => a.MapFrom(tp => tp.Student))
                .ForMember(d => d.Parent, a => a.MapFrom(tp => tp.Parent))
                .ForMember(d => d.Test, a => a.MapFrom(tp => tp.Test));

            CreateProjection<TestResponse, TestResponseResponseDTO>()
                .ForMember(d => d.TestScoreRankResult, a => a.MapFrom(tp => tp.TestScoreRankResult))
                .ForMember(d => d.TotalScore, a => a.MapFrom(tp => tp.TotalScore))
                .ForMember(d => d.Student, a => a.MapFrom(tp => tp.Student))
                .ForMember(d => d.Parent, a => a.MapFrom(tp => tp.Parent))
                .ForMember(d => d.Test, a => a.MapFrom(tp => tp.Test))
                .ForMember(d => d.TestResponseItems, a => a.MapFrom(tp => tp.TestResponseItems.Select(tri => tri)));

            // Map
            CreateMap<CreateTestResponseCommand, TestResponse>()
                .ForMember(d => d.TestScoreRankResult, a => a.MapFrom(tp => tp.TestScoreRankResult))
                .ForMember(d => d.TotalScore, a => a.MapFrom(tp => tp.TotalScore))
                .ForMember(d => d.StudentId, a => a.MapFrom(tp => tp.StudentId))
                .ForMember(d => d.ParentId, a => a.MapFrom(tp => tp.ParentId))
                .ForMember(d => d.TestId, a => a.MapFrom(tp => tp.TestId))
                .ForMember(d => d.TestResponseItems, a => a.MapFrom(tp => tp.TestResponseItems));

            CreateMap<TestResponse, TestResponseOverviewResponseDTO>()
                .ForMember(d => d.TestScoreRankResult, a => a.MapFrom(tp => tp.TestScoreRankResult))
                .ForMember(d => d.TotalScore, a => a.MapFrom(tp => tp.TotalScore))
                .ForMember(d => d.Student, a => a.MapFrom(tp => tp.Student))
                .ForMember(d => d.Parent, a => a.MapFrom(tp => tp.Parent))
                .ForMember(d => d.Test, a => a.MapFrom(tp => tp.Test));
        }
    }
}
