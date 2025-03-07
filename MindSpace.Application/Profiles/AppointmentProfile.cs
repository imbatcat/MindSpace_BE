using AutoMapper;
using MindSpace.Application.DTOs.Notifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentNotificationResponseDTO>();
        }
    }
}
