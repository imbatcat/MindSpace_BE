using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.MeetingRooms.Commands.CreateMeetingRoom;
using MindSpace.Application.Features.MeetingRooms.Commands.DeleteMeetingRoom;
using MindSpace.Application.Features.MeetingRooms.Queries.CheckIsRoomExists;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.EmailServices;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.API.Controllers
{
    // TESTING CONTROLLER FOR WEB RTC WILL BE REMOVED IN THE FUTURE
    public class WebRTCController(
        IMediator _mediator,
        IBackgroundJobService _backgroundJobService,
        IUnitOfWork UnitOfWork,
        IEmailService _emailService
    ) : BaseApiController
    {
        [HttpGet("rooms/{roomId}/exists")]
        public async Task<IActionResult> CheckIsRoomExists([FromRoute] string roomId)
        {
            try
            {
                var isActive = await _mediator.Send(new CheckIsRoomExistsQuery() { RoomId = roomId });

                return Ok(new { IsActive = isActive });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("rooms/{roomId}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] string roomId)
        {
            try
            {
                var isRoomExists = await _mediator.Send(new CheckIsRoomExistsQuery() { RoomId = roomId });

                if (!isRoomExists)
                {
                    return NotFound(new { Message = $"Room {roomId} does not exist." });
                }

                await _mediator.Send(new DeleteMeetingRoomCommand { RoomIdsToDelete = new List<string> { roomId } });

                return NoContent(); // Successfully deleted
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the room.");
            }
        }

        [HttpPost("rooms/{id}")]
        public async Task<IActionResult> CreateRoom([FromRoute] int id, [FromQuery] string studentEmail)
        {
            var spec = new AppointmentSpecification(id, AppointmentSpecification.IntParameterType.AppointmentId);
            var appointment = await UnitOfWork.Repository<Appointment>().GetBySpecAsync(spec);
            if (appointment == null)
            {
                return BadRequest("Appointment cannot be null.");
            }
            try
            {
                var newMeetingRoom = await _mediator.Send(new CreateMeetingRoomCommand()
                {
                    Appointment = appointment,
                    StartDate = DateTime.Now
                });

                appointment.MeetingRoomId = newMeetingRoom.Id;
                UnitOfWork.Repository<Appointment>().Update(appointment);
                await UnitOfWork.CompleteAsync();

                await _emailService.SendEmailAsync(studentEmail, "Meeting Room Created", $"Meeting Room Created {newMeetingRoom.MeetUrl}");

                return Created(newMeetingRoom.MeetUrl, new { RoomId = newMeetingRoom.MeetUrl });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the room.");
            }
        }
    }
}