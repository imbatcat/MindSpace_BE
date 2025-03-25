using MediatR;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Specifications.MeetingRoomSpecifications;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.MeetingRooms.Queries.GetMeetingRooms;
public class GetMeetingRoomsQueryHandler(
        IUnitOfWork _unitOfWork,
        ILogger<GetMeetingRoomsQueryHandler> _logger
    ) : IRequestHandler<GetMeetingRoomsQuery, List<MeetingRoom>>
{
    public async Task<List<MeetingRoom>> Handle(GetMeetingRoomsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all meeting rooms");
        var specficiation = new MeetingRoomSpecification(request.Date);
        var rooms = await _unitOfWork.Repository<MeetingRoom>().GetAllWithSpecAsync(specficiation);

        _logger.LogInformation("Found {Count} meeting rooms for {Date}", rooms.Count, DateOnly.FromDateTime(request.Date));

        return rooms.ToList();
    }
}
