using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.DTOs.Appointments
{
    public class PsychologistScheduleResponseDTO
    {
        public int? Id { get; set; }
        public int? PsychologistId { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public DateOnly? Date { get; set; }
        public PsychologistScheduleStatus? Status { get; set; }
    }
}
