using MediatR;
using MindSpace.Application.DTOs.Appointments;

namespace MindSpace.Application.Features.AppointmentNotes.Queries.GetAppointmentNotesById
{
    public class GetAppointmentNotesByIdQuery : IRequest<AppointmentNotesDTO>
    {
        public int Id { get; set; }
        public GetAppointmentNotesByIdQuery(int id)
        {
            Id = id;
        }
    }
}
