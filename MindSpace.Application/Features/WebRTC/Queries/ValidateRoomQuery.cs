using MediatR;

namespace MindSpace.Application.Features.WebRTC.Queries;

public class ValidateRoomQuery : IRequest<bool>
{
    public string RoomId { get; set; }
}
