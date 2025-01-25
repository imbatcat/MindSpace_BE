using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Constants
{
    public enum PaymentMethod
    {
        [EnumMember(Value = "MOMO")]
        MOMO,
        [EnumMember(Value = "VNPAY")]
        VNPAY
    }
}
