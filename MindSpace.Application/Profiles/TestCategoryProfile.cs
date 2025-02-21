using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class TestCategoryProfile : Profile
    {
            public TestCategoryProfile()
        {
            CreateProjection<TestCategory, TestCategoryDTO>();
            CreateMap<TestCategory, TestCategoryDTO>();
        }
    }
}
