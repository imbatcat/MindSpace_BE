using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum PsychologistScheduleStatus
    {

        [EnumMember(Value = "Free")]
        Free,
        [EnumMember(Value = "Locked")]
        Locked,
        [EnumMember(Value = "Booked")]
        Booked
    }
}
