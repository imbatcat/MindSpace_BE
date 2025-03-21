using MediatR;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.MeetingRooms.Queries.GetMeetingRooms;
public class GetMeetingRoomsQuery : IRequest<List<MeetingRoom>>
{
    public DateTime Date { get; set; }
}