using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum PaymentType
    {
        [EnumMember(Value = "Purchase")]
        Purchase,
        [EnumMember(Value = "Refund")]
        Refund,
    }
}
