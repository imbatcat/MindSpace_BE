using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum QuestionFormats
    {
        [EnumMember(Value = "Multiple Choice")]
        MultipleChoice,
        [EnumMember(Value = "Text")]
        Text,
        //[EnumMember(Value = "Dropdown")]
        //Dropdown
    }
}
