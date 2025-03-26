using MediatR;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.AppointmentNotes.Commands.UpdateAppointmentNotes
{
    public class UpdateAppointmentNotesCommand : IRequest
    {
        public int AppointmentId { get; set; }
        public required string NotesTitle { get; set; }
        public required string KeyIssues { get; set; }
        public required string Suggestions { get; set; }
        public string? OtherNotes { get; set; }
        public bool IsNoteShown { get; set; } = false;
    }
}
