using AutoMapper;
using MindSpace.Application.DTOs;
using MindSpace.Domain.Entities;

namespace MindSpace.Application.Profiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateProjection<School, SchoolDTO>();
            CreateMap<School, SchoolDTO>();
        }
    }
}
