using MediatR;

namespace MindSpace.Application.Features.MeetingRooms.Commands.DeleteMeetingRoom
{
    public class DeleteMeetingRoomCommand : IRequest
    {
        public required List<string> RoomIdsToDelete { get; set; }
    }
}