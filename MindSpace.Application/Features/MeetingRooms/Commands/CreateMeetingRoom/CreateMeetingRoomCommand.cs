using MediatR;
using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Features.MeetingRooms.Commands.CreateMeetingRoom;

public class CreateMeetingRoomCommand : IRequest<MeetingRoom>
{
    public required Appointment Appointment { get; set; }
    public required DateTime StartDate { get; set; }
}
