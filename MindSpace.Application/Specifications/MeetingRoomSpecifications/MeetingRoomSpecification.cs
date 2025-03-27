using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Specifications.MeetingRoomSpecifications
{
    public class MeetingRoomSpecification : BaseSpecification<MeetingRoom>
    {
        public MeetingRoomSpecification(DateTime startDate) :
            base(
                x => x.StartDate.Date == startDate.Date &&
                x.StartDate.TimeOfDay <= TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59) &&
                x.StartDate.TimeOfDay >= TimeSpan.Zero
            )
        {
        }

        public MeetingRoomSpecification(string roomId) :
            base(x => roomId.ToLower().Trim().Equals(x.RoomId.ToLower().Trim()))
        {
            AddInclude(x => x.Appointment);
            AddInclude("Appointment.Psychologist");
        }
    }
}