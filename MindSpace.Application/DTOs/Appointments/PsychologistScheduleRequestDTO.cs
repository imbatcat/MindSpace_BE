namespace MindSpace.Application.DTOs.Appointments
{
    public class PsychologistScheduleRequestDTO
    {
        public string? WeekDay { get; set; }
        public List<TimeSlotDTO> TimeSlots = new List<TimeSlotDTO>(); // list timeslot cua moi ngay trong tuan
    }
}
