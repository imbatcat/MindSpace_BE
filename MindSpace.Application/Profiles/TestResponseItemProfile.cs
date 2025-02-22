using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class TestResponseItemProfile : Profile
    {
        public TestResponseItemProfile ()
        {
            CreateProjection<TestResponseItem, TestResponseItemResponseDTO>()
                .ForMember(d => d.Id, a => a.MapFrom(tri => tri.Id))
                .ForMember(d => d.QuestionContent, a => a.MapFrom(tri => tri.QuestionContent))
                .ForMember(d => d.AnswerText, a => a.MapFrom(tri => tri.AnswerText))
                .ForMember(d => d.Score, a => a.MapFrom(tri => tri.Score))
                .ForMember(d => d.TestResponseId, a => a.MapFrom(tri => tri.TestResponseId));
        }
    }
}
