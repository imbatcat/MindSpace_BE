using AutoMapper;
using MindSpace.Application.DTOs.SupportingPrograms;
using MindSpace.Application.Features.SupportingPrograms.Commands.CreateSupportingProgram;
using MindSpace.Application.Features.SupportingPrograms.Commands.PatchSupportingProgram;
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

            CreateMap<SupportingProgram, SupportingProgramResponseDTO>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Ward, opt => opt.MapFrom(src => src.Address.Ward))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Address.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.StartDateAt, opt => opt.MapFrom(src => src.StartDateAt.ToString("dd/MM/yyyy")));


            CreateMap<SupportingProgram, SupportingProgramSingleResponseDTO>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Ward, opt => opt.MapFrom(src => src.Address.Ward))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Address.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.PsychologistId, opt => opt.MapFrom(src => src.Psychologist.Id))
                .ForMember(dest => dest.PsychologistName, opt => opt.MapFrom(src => src.Psychologist.UserName))
                .ForMember(dest => dest.StartDateAt, opt => opt.MapFrom(src => src.StartDateAt.ToString("dd/MM/yyyy")));

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
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.SupportingProgram.Title))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.SupportingProgram.ThumbnailUrl))
                .ForMember(dest => dest.PdffileUrl, opt => opt.MapFrom(src => src.SupportingProgram.PdffileUrl))
                .ForMember(dest => dest.MaxQuantity, opt => opt.MapFrom(src => src.SupportingProgram.MaxQuantity))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.SupportingProgram.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.SupportingProgram.Address.Street))
                .ForMember(dest => dest.Ward, opt => opt.MapFrom(src => src.SupportingProgram.Address.Ward))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.SupportingProgram.Address.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.SupportingProgram.Address.PostalCode))
                .ForMember(dest => dest.StartDateAt, opt => opt.MapFrom(src => src.SupportingProgram.StartDateAt))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.SupportingProgram.IsActive));


            // =============================
            // === PATCH, POST
            // =============================

            CreateMap<CreateSupportingProgramCommand, SupportingProgram>()
                .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
                .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
                .ForPath(dest => dest.Address.Ward, opt => opt.MapFrom(src => src.Ward))
                .ForPath(dest => dest.Address.Province, opt => opt.MapFrom(src => src.Province))
                .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.PostalCode));

            // Fixing the int that always return 0 when null
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<PatchSupportingProgramCommand, SupportingProgram>()
                .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
                .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
                .ForPath(dest => dest.Address.Ward, opt => opt.MapFrom(src => src.Ward))
                .ForPath(dest => dest.Address.Province, opt => opt.MapFrom(src => src.Province))
                .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForAllMembers(opt => opt.PreCondition((src, dest, srcMember) => srcMember != null));
        }
    }
}