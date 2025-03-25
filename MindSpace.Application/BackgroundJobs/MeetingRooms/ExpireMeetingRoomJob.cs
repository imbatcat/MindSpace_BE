using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Features.MeetingRooms.Commands.DeleteMeetingRoom;
using MindSpace.Application.Features.MeetingRooms.Queries.GetMeetingRooms;
using Quartz;

namespace MindSpace.Application.BackgroundJobs.MeetingRooms;

public class ExpireMeetingRoomJob(
    ILogger<ExpireMeetingRoomJob> _logger,
    IMediator _mediator
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Deleting rooms");

        var roomsToDelete = await _mediator.Send(new GetMeetingRoomsQuery()
        {
            Date = DateTime.Now.AddSeconds(-10)
        });

        await _mediator.Send(new DeleteMeetingRoomCommand
        {
            RoomIdsToDelete = roomsToDelete.Select(room => room.Id.ToString()).ToList()
        });
    }
}
