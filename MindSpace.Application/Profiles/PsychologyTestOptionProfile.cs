using AutoMapper;
using MindSpace.Application.DTOs.Tests;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Application.Profiles
{
    public class PsychologyTestOptionProfile : Profile
    {
        public PsychologyTestOptionProfile()
        {
            CreateProjection<PsychologyTestOption, PsychologyTestOptionResponseDTO>();
        }
    }
}
