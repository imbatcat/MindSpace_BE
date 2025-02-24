using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.DTOs.Appointments
{
    public class PsychologistScheduleResponseDTO 
    {
        public int PsychologistId { get; set; }
        public string Date { get; set; }
        public string WeekDay { get; set; }
        public List<TimeSlotDTO> TimeSlots { get; set; } = new List<TimeSlotDTO>();
    }
}
