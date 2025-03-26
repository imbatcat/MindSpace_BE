using MediatR;
using MindSpace.Application.DTOs;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Specifications.AppointmentSpecifications;

namespace MindSpace.Application.Features.AppointmentNotes.Queries.GetAppointmentNotesByAccount
{
    public class GetAppointmentNotesByAccountQuery : IRequest<PagedResultDTO<AppointmentNotesDTO>>
    {
        public AppointmentNotesSpecParams Params { get; set; }
        public GetAppointmentNotesByAccountQuery(AppointmentNotesSpecParams specParams) {
            Params = specParams;
        }
    }
}
