using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Constants
{
    public enum TargetUser
    {
        [EnumMember(Value = "Everyone")]
        Everyone,
        [EnumMember(Value = "Student")]
        Student,
        [EnumMember(Value = "Parent")]
        Parent
    }
}
