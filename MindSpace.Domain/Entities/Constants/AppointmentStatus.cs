using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
