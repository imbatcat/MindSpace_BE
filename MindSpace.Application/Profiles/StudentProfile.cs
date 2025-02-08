using AutoMapper;
using MindSpace.Application.DTOs;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateProjection<Student, StudentDTO>();
        }
    }
}