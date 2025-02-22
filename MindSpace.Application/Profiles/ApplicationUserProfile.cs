using AutoMapper;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateProjection<SchoolManager, ApplicationUserResponseDTO>()
                .ForMember(d => d.School, a => a.MapFrom(u => u.School));

            CreateProjection<Student, ApplicationUserResponseDTO>()
                .ForMember(d => d.School, a => a.MapFrom(u => u.School));

            CreateProjection<Parent, ApplicationUserResponseDTO>();

            CreateProjection<ApplicationUser, ApplicationUserResponseDTO>()
                .ForMember(d => d.Id, a => a.MapFrom(u => u.Id))
                .ForMember(d => d.Email, a => a.MapFrom(u => u.Email))
                .ForMember(d => d.FullName, a => a.MapFrom(u => u.FullName))
                .ForMember(d => d.PhoneNumber, a => a.MapFrom(u => u.PhoneNumber))
                .ForMember(d => d.UserName, a => a.MapFrom(u => u.UserName))
                .ForMember(d => d.DateOfBirth, a => a.MapFrom(u => u.DateOfBirth))
                .ForMember(d => d.ImageUrl, a => a.MapFrom(u => u.ImageUrl))
                .ForMember(d => d.Status, a => a.MapFrom(u => u.Status));

            CreateMap<ApplicationUser, ApplicationUserProfileDTO>();
            CreateMap<ApplicationUser, PsychologistProfileDTO>();

            CreateMap<ApplicationUser, ApplicationUserResponseDTO>()
                .Include<SchoolManager, ApplicationUserResponseDTO>();
        }
    }
}