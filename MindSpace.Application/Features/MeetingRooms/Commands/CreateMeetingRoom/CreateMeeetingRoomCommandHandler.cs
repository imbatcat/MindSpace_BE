using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.MeetingRooms.Commands.CreateMeetingRoom;

public class CreateMeetingRoomCommandHandler(
    IUnitOfWork _unitOfWork,
    ILogger<CreateMeetingRoomCommandHandler> _logger,
    IConfiguration _configuration
) : IRequestHandler<CreateMeetingRoomCommand, MeetingRoom>
{
    public async Task<MeetingRoom> Handle(CreateMeetingRoomCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating meeting room");
        var roomId = Guid.NewGuid().ToString();
        MeetingRoom meetingRoom = new()
        {
            StartDate = request.StartDate,
            RoomId = roomId,
            MeetUrl = $"{_configuration.GetValue<string>("MeetUrl")}/{roomId}"
        };
        _unitOfWork.Repository<MeetingRoom>().Insert(meetingRoom);
        await _unitOfWork.CompleteAsync();
        _logger.LogInformation("Meeting room created {id}", roomId);

        return meetingRoom;
    }
}
