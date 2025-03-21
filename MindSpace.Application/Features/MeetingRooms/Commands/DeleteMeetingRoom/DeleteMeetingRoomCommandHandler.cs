using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.MeetingRooms.Commands.DeleteMeetingRoom
{
    public class DeleteMeetingRoomCommandHandler(
        IUnitOfWork _unitOfWork,
        ILogger<DeleteMeetingRoomCommandHandler> _logger
    ) : IRequestHandler<DeleteMeetingRoomCommand>
    {
        async Task IRequestHandler<DeleteMeetingRoomCommand>.Handle(DeleteMeetingRoomCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting {count} meeting room", request.RoomIdsToDelete.Count);

            foreach (var roomId in request.RoomIdsToDelete)
            {
                _unitOfWork.Repository<MeetingRoom>().Delete(int.Parse(roomId));
            }

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting meeting rooms {RoomIds}", request.RoomIdsToDelete);
            }
            _logger.LogInformation("{count} meeting rooms deleted", request.RoomIdsToDelete.Count);

        }
    }
}