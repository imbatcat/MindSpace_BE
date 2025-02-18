using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum AppointmentStatus
    {
        [EnumMember(Value = "Upcoming")]
        Upcoming,
        [EnumMember(Value = "Done")]
        Done,
        [EnumMember(Value = "Canceled")]
        Canceled
    }
}
