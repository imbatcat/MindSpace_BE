using AutoMapper;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserProfileDTO>();
            CreateMap<ApplicationUser, PsychologistProfileDTO>();
        }
    }
}