using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum AppointmentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Booked")]
        Booked,
        [EnumMember(Value = "Canceled")]
        Canceled
    }
}
