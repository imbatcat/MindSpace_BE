
using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.MeetingRoomSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.MeetingRooms.Queries.CheckIsRoomExists
{
    internal class CheckIsRoomExistsQueryHandler(
        IUnitOfWork _unitOfWork,
        ILogger<CheckIsRoomExistsQueryHandler> _logger
    ) : IRequestHandler<CheckIsRoomExistsQuery, bool>
    {
        public async Task<bool> Handle(CheckIsRoomExistsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Checking if room exists for appointment {RoomId}", request.RoomId);

            var specification = new MeetingRoomSpecification(request.RoomId);
            var room = await _unitOfWork.Repository<MeetingRoom>().GetBySpecAsync(specification);

            _logger.LogInformation("Room exists: {RoomExists}", room != null);

            return room != null;
        }
    }
}
