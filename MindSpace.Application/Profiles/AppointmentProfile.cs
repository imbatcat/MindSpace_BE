using AutoMapper;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.DTOs.Notifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentNotificationResponseDTO>();
            CreateMap<Appointment, AppointmentHistoryDTO>()
                .ForMember(d => d.MeetUrl, opt => opt.MapFrom(s => s.MeetingRoom!.MeetUrl))
                .ForMember(d => d.PsychologistName, opt => opt.MapFrom(s => s.Psychologist!.FullName))
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.PsychologistSchedule!.Date))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.PsychologistSchedule!.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.PsychologistSchedule!.EndTime));
        }
    }
}
