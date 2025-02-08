using AutoMapper;
using MindSpace.Application.DTOs;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Features.SupportingPrograms.Mappings
{
    public class SupportingProgramProfile : Profile
    {
        public SupportingProgramProfile()
        {
            CreateMap<SupportingProgram, SupportingProgramDTO>()
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address != null ? s.Address.Street : null))
                .ForMember(d => d.Province, o => o.MapFrom(s => s.Address != null ? s.Address.Province : null))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address != null ? s.Address.City : null))
                .ForMember(d => d.PostalCode, o => o.MapFrom(s => s.Address != null ? s.Address.PostalCode : null))
                .ForMember(d => d.Ward, o => o.MapFrom(s => s.Address != null ? s.Address.Ward : null));
        }
    }
}

