using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Constants;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.VideoCallServices;
using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Exceptions;
using Quartz;

namespace MindSpace.Application.BackgroundJobs.MeetingRooms;

public class CreateMeetingRoomJob(
    ILogger<CreateMeetingRoomJob> _logger,
    IUnitOfWork _unitOfWork,
    IWebRTCService _webRTCService,
    IBackgroundJobService _backgroundJobService
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Starting {bgJob}", nameof(CreateMeetingRoomJob));
        var sessionId = context.JobDetail.JobDataMap.GetString("referenceId");

        var appointment = await GetAppointment(sessionId);

        var (roomId, roomUrl) = _webRTCService.CreateRoom(appointment);

        var totalMinutes = GetMeetingEndTimeInMinutes(appointment);

        await _backgroundJobService.ScheduleJobWithFireOnce<ExpireMeetingRoomJob>(roomId, totalMinutes);

        _logger.LogInformation("Finished {bgJob}", nameof(CreateMeetingRoomJob));


        #region Helper Methods
        async Task<Appointment> GetAppointment(string sessionId)
        {
            var specification = new AppointmentSpecification(sessionId);
            return await _unitOfWork.Repository<Appointment>().GetBySpecAsync(specification) ??
                throw new NotFoundException(typeof(Appointment).Name, sessionId);
        }

        int GetMeetingEndTimeInMinutes(Appointment appointment)
        {
            var endDate = appointment.PsychologistSchedule.Date;
            var endTime = appointment.PsychologistSchedule.EndTime;

            // Calculate the end time for the meeting room, the room will be removed 5 minutes after the appointment actually ends
            var endDateTime = endDate.ToDateTime(endTime).AddMinutes(AppCts.WebRTC.RoomRemovalActualTimeInMinutes);

            return (int)(endDateTime - DateTime.Now).TotalMinutes;
        }
        #endregion
    }
}
