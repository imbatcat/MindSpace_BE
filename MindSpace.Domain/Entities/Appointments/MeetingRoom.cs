namespace MindSpace.Domain.Entities.Appointments;

public class MeetingRoom : BaseEntity
{
    public required string RoomId { get; set; }
    public required string MeetUrl { get; set; }
    public required DateTime StartDate { get; set; }
}
