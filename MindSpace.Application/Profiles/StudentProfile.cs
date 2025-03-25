using AutoMapper;
using MindSpace.Application.DTOs.ApplicationUsers;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateProjection<Student, StudentResponseDTO>()
                .ForMember(d => d.DateOfBirth, o => o.MapFrom<DateOnly?>(s =>
                            s.DateOfBirth.HasValue ? DateOnly.FromDateTime(s.DateOfBirth.Value) : null));

            CreateMap<Student, StudentResponseDTO>()
                .ForMember(d => d.DateOfBirth, o => o.MapFrom<DateOnly?>(s =>
                            s.DateOfBirth.HasValue ? DateOnly.FromDateTime(s.DateOfBirth.Value) : null));
        }
    }
}