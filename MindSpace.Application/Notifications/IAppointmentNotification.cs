using MindSpace.Application.DTOs.Notifications;

namespace MindSpace.Application.Notifications
{
    public interface IAppointmentNotification
    {
        public Task NotifyAppointmentPending(AppointmentNotificationResponseDTO appointmentNotificationResponseDTO);
        public Task NotifyAppointmentSuccess(AppointmentNotificationResponseDTO appointmentNotificationResponseDTO);
        public Task NotifyAppointmentFailed(AppointmentNotificationResponseDTO appointmentNotificationResponseDTO);
    }
}