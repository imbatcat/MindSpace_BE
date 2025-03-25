using AutoMapper;
using MindSpace.Application.DTOs;
using MindSpace.Domain.Entities;

namespace MindSpace.Application.Profiles
{
    public class SpecializationProfile : Profile
    {
        public SpecializationProfile()
        {
            CreateProjection<Specialization, SpecializationDTO>();
            CreateMap<Specialization, SpecializationDTO>();
        }
    }
}
