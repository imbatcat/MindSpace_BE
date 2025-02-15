using System.Runtime.Serialization;

namespace MindSpace.Domain.Entities.Constants
{
    public enum ResourceType
    {
        [EnumMember(Value = "Blog")]
        Blog,

        [EnumMember(Value = "Article")]
        Article,
    }
}
