using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.DTOs.Appointments
{
    public class TimeSlotResponseDTO
    {
        public int? Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string? Date { get; set; }
        public int? PsychologistId { get; set; }
        public PsychologistScheduleStatus? Status { get; set; }
    }
}
