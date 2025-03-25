using MediatR;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.MeetingRooms.Commands.UpdateMeetingRoom;

public class UpdateMeetingRoomCommand : IRequest<MeetingRoom>
{
    public required MeetingRoom MeetingRoom { get; set; }
}
