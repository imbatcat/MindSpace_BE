using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class TestScoreRankProfile : Profile
    {
        public TestScoreRankProfile()
        {
            CreateProjection<TestScoreRank, TestScoreRankResponseDTO>()
               .ForMember(d => d.TestId, a => a.MapFrom(t => t.TestId))
               .ForMember(d => d.MinScore, a => a.MapFrom(t => t.MinScore))
               .ForMember(d => d.MaxScore, a => a.MapFrom(t => t.MaxScore))
               .ForMember(d => d.Result, a => a.MapFrom(t => t.Result));


        }
    }
}
