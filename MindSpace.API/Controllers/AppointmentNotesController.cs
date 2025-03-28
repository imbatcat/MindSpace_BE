using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.API.RequestHelpers;
using MindSpace.Application.DTOs.Appointments;
using MindSpace.Application.Features.AppointmentNotes.Commands.UpdateAppointmentNotes;
using MindSpace.Application.Features.AppointmentNotes.Queries.GetAppointmentNotesByAccount;
using MindSpace.Application.Features.AppointmentNotes.Queries.GetAppointmentNotesById;
using MindSpace.Application.Specifications.AppointmentSpecifications;

namespace MindSpace.API.Controllers
{
    [Route("api/v{version:apiVersion}/appointment-notes")]
    public class AppointmentNotesController(IMediator mediator) : BaseApiController
    {
        // GET /api/v1/appointment-notes/{id}
        // Get appointment notes by appointment ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppointmentNotesDTO>> GetAppointmentNotesById(int id)
        {
            var appointmentNotesResponse = await mediator.Send(new GetAppointmentNotesByIdQuery(id));
            return Ok(appointmentNotesResponse);
        }

        // GET /api/v1/appointment-notes?accountId={accountId}
        // Get all appointment notes for a specific account (student/psychologist)
        [HttpGet()]
        public async Task<ActionResult<IReadOnlyList<AppointmentNotesDTO>>> GetAppointmentNotesByAccount([FromQuery] AppointmentNotesSpecParams specParams)
        {
            var appointmentNotesListResponse = await mediator.Send(new GetAppointmentNotesByAccountQuery(specParams));
            return Ok(appointmentNotesListResponse);
        }

        // ==============================
        // === POST, PUT, DELETE, PATCH
        // ==============================

        // PUT /api/v1/appointment-notes
        // Update appointment notes for a specific appointment
        [InvalidateCache("/api/appointment-notes|")]
        [HttpPut]
        public async Task<ActionResult> UpdateAppointmentNotes([FromBody] UpdateAppointmentNotesCommand command)
        {
            await mediator.Send(command);
            return Ok("Appointment notes updated successfully!");
        }
    }
}
