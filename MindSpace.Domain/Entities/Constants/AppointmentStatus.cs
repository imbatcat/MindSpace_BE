using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum AppointmentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Paid")]
        Paid,
        [EnumMember(Value = "Done")]
        Done,
        [EnumMember(Value = "Canceled")]
        Canceled
    }
}
