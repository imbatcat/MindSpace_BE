using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Constants
{
    public enum FixedType
    {
        [EnumMember(Value = "Difficulty")]
        Difficulty,
        [EnumMember(Value = "Likert")]
        Likert,
        [EnumMember(Value = "Frequency")]
        Frequency
    }
}
