using System.Runtime.Serialization;

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
