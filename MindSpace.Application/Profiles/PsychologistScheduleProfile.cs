using AutoMapper;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Profiles
{
    public class PsychologistScheduleProfile : Profile
    {
        public PsychologistScheduleProfile()
        {
            CreateProjection<PsychologistSchedule, TimeSlotResponseDTO>()
            .ForMember(d => d.Id, a => a.MapFrom(p => p.Id))
            .ForMember(d => d.PsychologistId, a => a.MapFrom(p => p.PsychologistId))
            .ForMember(d => d.StartTime, a => a.MapFrom(p => p.StartTime))
            .ForMember(d => d.EndTime, a => a.MapFrom(p => p.EndTime))
            .ForMember(d => d.Date, a => a.MapFrom(p => p.Date))
            .ForMember(d => d.Status, a => a.MapFrom(p => p.Status));
        }
    }
}
