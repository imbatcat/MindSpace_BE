using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Domain.Entities.Appointments;
using System;

namespace MindSpace.Application.Features.MeetingRooms.Commands.UpdateMeetingRoom;

public class UpdateMeetingRoomCommandHandler(
    IUnitOfWork _unitOfWork,
    ILogger<UpdateMeetingRoomCommandHandler> _logger
) : IRequestHandler<UpdateMeetingRoomCommand, MeetingRoom>
{
    public async Task<MeetingRoom> Handle(UpdateMeetingRoomCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating meeting room {id}", request.MeetingRoom.Id);

        _unitOfWork.Repository<MeetingRoom>().Update(request.MeetingRoom);
        await _unitOfWork.CompleteAsync();

        _logger.LogInformation("Meeting room updated {id}", request.MeetingRoom.Id);
        return request.MeetingRoom;
    }
}