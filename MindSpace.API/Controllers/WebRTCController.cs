using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindSpace.Application.Features.WebRTC.Queries;

namespace MindSpace.API.Controllers
{
    public class WebRTCController(
        IMediator _mediator
    ) : BaseApiController
    {
        [HttpGet("rooms/{roomId}/validate")]
        public async Task<IActionResult> ValidateRoom([FromRoute] string roomId)
        {
            try
            {
                var isActive = await _mediator.Send(new ValidateRoomQuery { RoomId = roomId });

                return Ok(new { IsActive = isActive });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
