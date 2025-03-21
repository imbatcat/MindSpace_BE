using MediatR;

namespace MindSpace.Application.Features.MeetingRooms.Queries.CheckIsRoomExists
{
    public class CheckIsRoomExistsQuery : IRequest<bool>
    {
        public required string RoomId { get; set; }
    }
}
