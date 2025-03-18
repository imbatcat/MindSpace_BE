using AutoMapper;
using MindSpace.Application.DTOs;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Owned;

namespace MindSpace.Application.Profiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateProjection<School, SchoolDTO>();
            CreateMap<School, SchoolDTO>();

            CreateProjection<Address, AddressDTO>();
            CreateMap<Address, AddressDTO>();
        }
    }
}
