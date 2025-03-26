namespace MindSpace.Application.DTOs.Appointments;

public class PsychologistAppointmentHistoryDTO
{
    public required DateOnly Date { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
    public string? StudentName { get; set; }
    public required bool IsUpcoming { get; set; }
    public required string MeetUrl { get; set; }
}