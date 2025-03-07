using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.DTOs.Notifications
{
    public class AppointmentNotificationResponseDTO
    {
        public int StudentId { get; set; }
        public int PsychologistId { get; set; }
        public int PsychologistScheduleId { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
