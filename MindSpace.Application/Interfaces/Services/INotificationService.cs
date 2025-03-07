using MindSpace.Application.DTOs.Notifications;

namespace MindSpace.Application.Interfaces.Services
{
    public interface INotificationService
    {
        public Task NotifyAppointmentPending(string groupRole, AppointmentNotificationResponseDTO payload);
        public Task NotifyAppointmentSuccess(string groupRole, AppointmentNotificationResponseDTO payload);
        public Task NotifyAppointmentFailed(string groupRole, AppointmentNotificationResponseDTO payload);
        public Task NotifyPsychologistScheduleFree(string groupRole, PsychologistScheduleNotificationResponseDTO payload);
        public Task NotifyPsychologistScheduleLocked(string groupRole, PsychologistScheduleNotificationResponseDTO payload);
        public Task NotifyPsychologistScheduleBooked(string groupRole, PsychologistScheduleNotificationResponseDTO payload);
    }
}
