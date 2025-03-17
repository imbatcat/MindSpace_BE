using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Services.VideoCallServices;
using Quartz;
using System;

namespace MindSpace.Application.BackgroundJobs.MeetingRooms;

public class ExpireMeetingRoomJob(
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
            return Task.CompletedTask;
        }

        _logger.LogInformation("Room {RoomId} is active", roomId);

        _webRTCService.DeleteRoom(roomId);

        _logger.LogInformation("Room {RoomId} deleted", roomId);

        return Task.CompletedTask;
    }
}
