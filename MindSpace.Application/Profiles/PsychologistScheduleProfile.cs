using AutoMapper;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.DTOs.Notifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Profiles
{
    public class PsychologistScheduleProfile : Profile
    {
        public PsychologistScheduleProfile()
        {
            CreateProjection<PsychologistSchedule, TimeSlotDTO>()
                .ForMember(d => d.Id, a => a.MapFrom(p => p.Id))
                .ForMember(d => d.PsychologistId, a => a.MapFrom(p => p.PsychologistId))
                .ForMember(d => d.StartTime, a => a.MapFrom(p => p.StartTime))
                .ForMember(d => d.EndTime, a => a.MapFrom(p => p.EndTime))
                .ForMember(d => d.Date, a => a.MapFrom(p => p.Date))
                .ForMember(d => d.Status, a => a.MapFrom(p => p.Status));

            CreateMap<TimeSlotDTO, PsychologistSchedule>()
                .ForMember(d => d.Id, a => a.Ignore())
                .ForMember(d => d.StartTime, a => a.MapFrom(p => TimeOnly.Parse(p.StartTime)))
                .ForMember(d => d.EndTime, a => a.MapFrom(p => TimeOnly.Parse(p.EndTime)))
                .ForMember(d => d.Date, a => a.MapFrom(p => string.IsNullOrEmpty(p.Date) ? (DateOnly?)null : DateOnly.Parse(p.Date)))
                .ForMember(d => d.PsychologistId, a => a.Ignore())
                .ForMember(d => d.Status, a => a.Ignore());

            CreateMap<PsychologistSchedule, PsychologistScheduleNotificationResponseDTO>();
        }
    }
}
