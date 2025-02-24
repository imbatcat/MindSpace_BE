namespace MindSpace.Application.DTOs.Appointments
{
    public class PsychologistScheduleRequestDTO
    {
        public string WeekDay { get; set; }
        public List<TimeSlotResponseDTO> TimeSlots = new List<TimeSlotResponseDTO>();
    }
}
