using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Constants
{
    public enum PsycholigistScheduleStatus
    {
        [EnumMember(Value = "Free")]
        Free,
        [EnumMember(Value = "Booked")]
        Booked
    }
}
