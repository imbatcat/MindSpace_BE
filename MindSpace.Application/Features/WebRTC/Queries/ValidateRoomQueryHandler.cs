using MediatR;
using MindSpace.Application.Interfaces.Services.VideoCallServices;

namespace MindSpace.Application.Features.WebRTC.Queries;

public class ValidateRoomQueryHandler(
    IWebRTCService _webRTCService
) : IRequestHandler<ValidateRoomQuery, bool>
{
    public Task<bool> Handle(ValidateRoomQuery request, CancellationToken cancellationToken)
    {
        var isActive = _webRTCService.IsRoomActive(request.RoomId);

        return Task.FromResult(isActive);
    }
}
