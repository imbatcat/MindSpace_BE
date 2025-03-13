using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.DTOs.Notifications
{
    public class PsychologistScheduleNotificationResponseDTO
    {
        public int ScheduleId { get; set; }
        public PsychologistScheduleStatus Status { get; set; }
    }
}
