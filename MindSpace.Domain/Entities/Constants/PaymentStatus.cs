using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum PaymentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Success")]
        Success,
        [EnumMember(Value = "Failed")]
        Failed
    }
}
