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

        // account infos
        public int? PsychologistId { get; set; }
        public string? PsychologistName { get; set; }
        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentImageUrl { get; set; }
        public string? PsychologistImageUrl { get; set; }
    }
}
