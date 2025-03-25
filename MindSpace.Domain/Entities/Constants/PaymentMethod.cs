using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum PaymentMethod
    {
        [EnumMember(Value = "MOMO")]
        MOMO,
        [EnumMember(Value = "VNPAY")]
        VNPAY,
        [EnumMember(Value = "STRIPE")]
        STRIPE
    }
}
