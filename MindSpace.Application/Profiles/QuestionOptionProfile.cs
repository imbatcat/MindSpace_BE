using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class QuestionOptionProfile : Profile
    {
        public QuestionOptionProfile()
        {
            CreateProjection<QuestionOption, QuestionOptionResponseDTO>();
        }
    }
}