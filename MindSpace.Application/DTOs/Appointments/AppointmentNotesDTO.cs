namespace MindSpace.Application.DTOs.Appointments
{
    public class AppointmentNotesDTO
    {
        public int? AppointmentId { get; set; }
        public string? NotesTitle { get; set; }
        public string? KeyIssues { get; set; }
        public string? Suggestions { get; set; }
        public string? OtherNotes { get; set; }
        public bool? IsNoteShown { get; set; }
    }
}
