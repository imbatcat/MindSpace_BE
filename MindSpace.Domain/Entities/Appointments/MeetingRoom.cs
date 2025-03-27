namespace MindSpace.Domain.Entities.Appointments;

public class MeetingRoom : BaseEntity
{
    public required string RoomId { get; set; }
    public required string MeetUrl { get; set; }
    public required DateTime StartDate { get; set; }

    // 1 Appointment - 1 Meeting Room
    public virtual Appointment Appointment { get; set; }
}
