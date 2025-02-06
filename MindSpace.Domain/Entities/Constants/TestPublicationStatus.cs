using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum TestPublicationStatus
    {
        [EnumMember(Value = "Enabled")]
        Enabled,

        [EnumMember(Value = "Disabled")]
        Disabled
    }
}