using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.WebRTC.Queries;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.VideoCallServices;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Infrastructure.Services.VideoCallServices;
using Quartz;

namespace MindSpace.API.Controllers
{
    public class WebRTCController(
        IMediator _mediator,
        IWebRTCService _webRTCService,
        IBackgroundJobService _backgroundJobService,
        IUnitOfWork UnitOfWork
    ) : BaseApiController
    {
        [HttpGet("rooms/{roomId}/validate")]
        public async Task<IActionResult> ValidateRoom([FromRoute] string roomId)
        {
            try
            {
                var isActive = await _mediator.Send(new ValidateRoomQuery { RoomId = roomId });

                return Ok(new { IsActive = isActive });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("rooms/{id}")]
        public async Task<IActionResult> CreateRoom([FromRoute] int id)
        {
            var spec = new AppointmentSpecification(id);
            var appointment = await UnitOfWork.Repository<Appointment>().GetBySpecAsync(spec);
            if (appointment == null)
            {
                return BadRequest("Appointment cannot be null.");
            }
            try
            {
                var (roomId, roomUrl) = _webRTCService.CreateRoom(appointment);
                await _backgroundJobService.ScheduleJobWithFireOnce<ExpireMeetingRoomJob>(roomId, 10);

                return Created(roomUrl, new { RoomId = roomId, RoomUrl = roomUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the room.");
            }
        }

        [HttpGet("rooms/active")]
        public IActionResult GetActiveRooms()
        {
            var activeRooms = WebRTCService.ActiveRooms;
            return Ok(activeRooms);
        }


        class ExpireMeetingRoomJob(
            ILogger<ExpireMeetingRoomJob> _logger,
            IWebRTCService _webRTCService
        ) : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                var roomId = context.JobDetail.JobDataMap.GetString("referenceId");
                var isRoomActive = _webRTCService.IsRoomActive(roomId);
                if (!isRoomActive)
                {
                    _logger.LogInformation("Room {RoomId} is not active", roomId);
                    _webRTCService.DeleteRoom(roomId);
                    _logger.LogInformation("Room {RoomId} deleted", roomId);
                }
                else _logger.LogInformation("Room {RoomId} is active", roomId);

                return Task.CompletedTask;
            }
        }
    }
}
