using MindSpace.Application.DTOs.Notifications;

namespace MindSpace.Application.Notifications
{
    public interface IPsychologistScheduleNotification
    {
        public Task NotifyPsychologistScheduleFree(PsychologistScheduleNotificationResponseDTO payload);
        public Task NotifyPsychologistScheduleLocked(PsychologistScheduleNotificationResponseDTO payload);
        public Task NotifyPsychologistScheduleBooked(PsychologistScheduleNotificationResponseDTO payload);
    }
}