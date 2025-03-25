namespace MindSpace.Application.DTOs.Appointments;

public class AppointmentHistoryDTO
{
    public required DateOnly Date { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
    public required string PsychologistName { get; set; }
    public required bool IsUpcoming { get; set; }
    public required string MeetUrl { get; set; }
}
