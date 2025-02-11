using AutoMapper;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Commands;
using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Application.Profiles
{
    public class SupportingProgramProfile : Profile
    {
        public SupportingProgramProfile()
        {
            // =============================
            // === GET
            // =============================

            CreateProjection<SupportingProgram, SupportingProgramResponseDTO>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Ward, opt => opt.MapFrom(src => src.Address.Ward))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Address.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode));


            CreateProjection<SupportingProgram, SupportingProgramWithStudentsResponseDTO>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Ward, opt => opt.MapFrom(src => src.Address.Ward))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Address.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(d => d.Students, o => o.MapFrom(
                            s => s.SupportingProgramHistories.Select(h => h.Student))); // If Using Projection then AutoMapper will map with StudentResponseDTO based on 
                                                                                        // the type of list of students in SupportingProgramResponseDT

            CreateProjection<SupportingProgramHistory, SupportingProgramResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SupportingProgram.Id))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.SupportingProgram.ThumbnailUrl))
                .ForMember(dest => dest.PdffileUrl, opt => opt.MapFrom(src => src.SupportingProgram.PdffileUrl))
                .ForMember(dest => dest.MaxQuantity, opt => opt.MapFrom(src => src.SupportingProgram.MaxQuantity))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.SupportingProgram.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.SupportingProgram.Address.Street))
                .ForMember(dest => dest.Ward, opt => opt.MapFrom(src => src.SupportingProgram.Address.Ward))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.SupportingProgram.Address.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.SupportingProgram.Address.PostalCode))
                .ForMember(dest => dest.StartDateAt, opt => opt.MapFrom(src => src.SupportingProgram.StartDateAt));

            // =============================
            // === PATCH, POST
            // =============================

            CreateMap<CreateSupportingProgramCommand, SupportingProgram>()
                .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
                .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
                .ForPath(dest => dest.Address.Ward, opt => opt.MapFrom(src => src.Ward))
                .ForPath(dest => dest.Address.Province, opt => opt.MapFrom(src => src.Province))
                .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.PostalCode));

        }
    }
}