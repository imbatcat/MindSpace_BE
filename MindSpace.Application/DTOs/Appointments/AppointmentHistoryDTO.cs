namespace MindSpace.Application.DTOs.Appointments;

public class AppointmentHistoryDTO
{
    public int Id { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
    public int? PsychologistId { get; set; }
    public string? PsychologistName { get; set; }
    public int? StudentId { get; set; }
    public string? StudentName { get; set; }
    public required bool IsUpcoming { get; set; }
    public required string MeetUrl { get; set; }
    public string? Status { get; set; }
}
