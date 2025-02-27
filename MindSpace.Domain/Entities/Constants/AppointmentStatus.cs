using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum AppointmentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Success")]
        Success,
        [EnumMember(Value = "Failed")]
        Failed,
    }
}
