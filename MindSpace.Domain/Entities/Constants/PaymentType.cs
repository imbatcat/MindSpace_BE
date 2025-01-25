using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Constants
{
    public enum PaymentType
    {
        [EnumMember(Value = "Purchase")]
        Purchase,
        [EnumMember(Value = "Refund")]
        Refund
    }
}
